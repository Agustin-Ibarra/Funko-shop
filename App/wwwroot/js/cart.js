const $body = document.querySelector("body");
const $cartList = document.querySelector(".cart-list");
const $quantity = document.getElementById("qunatity");
const $subtotal = document.getElementById("subtotal");
const $total = document.getElementById("total");
const $cartLoader = document.querySelector(".cart-item-loader");
const $cartEmpty = document.querySelector(".cart-item-empty");
const $pay = document.querySelector(".pay");

fetch("/cart/items")
.then(async(response)=>{
  if(response.status === 404){
    $cartEmpty.classList.remove("hidden");
  }
  else if(response.status === 200){
    $pay.classList.remove("hidden");
    const itemCart = await response.json();
    itemCart.itemsCart.forEach(item => {
      const $liCart = document.createElement("li");
      const $divData = document.createElement("div");
      const $divImage = document.createElement("div");
      const $image = document.createElement("img");
      const $divCartData = document.createElement("div");
      const $name = document.createElement("p");
      const $category = document.createElement("p");
      const $price = document.createElement("p");
      const $quantity = document.createElement("p");
      const $subtotal = document.createElement("p");
      const $divDelete = document.createElement("div");
      const $span = document.createElement("span");
      $cartList.appendChild($liCart);
      $liCart.setAttribute("class","cart-item");
      $liCart.setAttribute("id",item.idItem);
      $liCart.appendChild($divData);
      $liCart.appendChild($quantity);
      $liCart.appendChild($subtotal);
      $liCart.appendChild($divDelete);
      $divData.setAttribute("class","div-img-data");
      $divData.appendChild($divImage);
      $divData.appendChild($divCartData);
      $divCartData.setAttribute("class","cart-item-data");
      $divCartData.appendChild($name);
      $divCartData.appendChild($category);
      $divCartData.appendChild($price);
      $divImage.setAttribute("class","cart-div-img");
      $divImage.appendChild($image);
      $divDelete.setAttribute("class","delete-item-div");
      $divDelete.appendChild($span);
      $image.setAttribute("src",`/images/${item.image}`);
      $name.setAttribute("class","cart-item-name");
      $name.textContent = item.item;
      $category.setAttribute("class","cart-category");
      $category.textContent = item.category;
      $price.setAttribute("class","cart-data-price");
      $price.textContent = `$${Number(item.price).toFixed(2)}`;
      $quantity.setAttribute("class","cart-amount");
      $quantity.textContent = item.quantity;
      $subtotal.setAttribute("class","cart-price");
      $subtotal.textContent = `$${Number(item.totalCart).toFixed(2)}`;
      $span.setAttribute("class","material-symbols-outlined delete-item");
      $span.setAttribute("id",item.idCart);
      $span.textContent = "delete";
    });
    $quantity.textContent = itemCart.items;
    $total.textContent = `$${Number(itemCart.totalToPay).toFixed(2)}`;
    $subtotal.textContent = `$${Number(itemCart.totalToPay).toFixed(2)}`;
  }
})
.finally(()=>{
  $cartLoader.classList.add("hidden");
});

$body.addEventListener("click",(e)=>{
  if(e.target.matches(".delete-item")){
    e.target.parentNode.childNodes[0].textContent = "";
    const $spinner = document.createElement("div");
    $spinner.setAttribute("class","spinner delete-spinner");
    e.target.parentNode.appendChild($spinner);
    fetch(`/cart/items/${e.target.id}`,
      {method:"DELETE"}
    )
    .then(async(response)=>{
      if(response.status === 204){
        window.location.reload();
      }
      else{
        error = await response.json();
        console.log(error);
      }
    })
    .catch((error)=>{
      console.error(error);
    })
    .finally(()=>{
      e.target.parentNode.childNodes[0].textContent = "delete";
      $spinner.setAttribute("class","spinner delete-spinner");
      e.target.parentNode.removeChild($spinner);
    });
  }
  else if(e.target.matches(".pay")){
    const $payIcon = document.querySelector(".pay-icon");
    const $spinner = document.querySelector(".pay-spinner");
    const items = document.querySelectorAll(".cart-item");
    $payIcon.textContent = "";
    $spinner.classList.remove("hidden");
    const itemsList = [];
    items.forEach(item => {
      if(item.id && item.childNodes[2].textContent){
        const unitPrice = item.childNodes[0].childNodes[1].childNodes[2].textContent.replace("$","");
        const itemData = {idItem:0,quantity:0,unitPrice:unitPrice}
        itemData.idItem = item.id;
        itemData.quantity = item.childNodes[1].textContent;
        itemsList.push(itemData);
      }
    });
    fetch("/item",{
      method : "PATCH",
      headers:{"Content-Type":"application/json"},
      body:JSON.stringify({items:itemsList})
    })
    .catch((error)=>{
      console.log(error);
    });

    fetch("/purchase",{
      method:"POST",
      headers:{"Content-Type":"application/json"},
      body:JSON.stringify({items:itemsList,total:$total.textContent.replace("$","")})
    })
    .then(async(response)=>{
      const idPurchase = await response.json();
      console.log(idPurchase);
      sessionStorage.setItem("idPurchase",idPurchase.id);
      window.location.href = "/purchases";
    })
    .catch((error)=>{
      console.log(error);
    })
    .finally(()=>{
      $payIcon.textContent = "payments";
      $spinner.classList.add("hidden");
    });
  }
});