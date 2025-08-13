const $body = document.querySelector("body");
const $quantity = document.querySelector(".quantity-text");
const $addCart = document.querySelector(".add-cart");
const $add = document.querySelector(".add");
const $less = document.querySelector(".less");
const $cart = document.querySelector(".cart-add-text");
const $spinner = document.querySelector(".big-spinner");
const $miniSpinner = document.querySelector(".spinner");
const $cartIcon = document.querySelector(".material-symbols-outlined");
let quantity = 1;
let limit = 5;

fetch(`/item/details/${sessionStorage.getItem("id")}`)
.then(async(response)=>{
  const item = await response.json();
  const $img = document.querySelector(".item-choice-img");
  const $category = document.querySelector(".item-choice-category");
  const $name = document.querySelector(".item-choice-name");
  const $description = document.querySelector(".item-choice-description");
  const $pirce = document.querySelector(".item-choice-price");
  $addCart.setAttribute("id",item.idItem);
  $img.setAttribute("src",`/images/${item.image}`);
  $category.textContent = item.categoryName;
  $name.textContent = item.itemName;
  $description.textContent = item.description;
  $pirce.textContent = `$${Number(item.price).toFixed(2)}`;
  if(item.stock < 5 && item.stock > 0){
    limit = item.stock;
  }
  else if(item.stock == 0){
    $addCart.classList.add('hidden');
    const $soldOut = document.querySelector(".sold-out");
    $soldOut.classList.remove("hidden");
    $quantity.textContent = 0;
    limit = 0;
  }
})
.catch((error)=>{
  console.error(error)
})
.finally(()=>{
  $spinner.classList.add("hidden");
});

$body.addEventListener("click",(e)=>{
  if(e.target.matches(".add")){
    if(quantity < limit){
      quantity = quantity + 1;
      $quantity.textContent = quantity; 
    }
  }
  else if(e.target.matches(".less")){
    if(quantity > 1){
      quantity = quantity - 1;
      $quantity.textContent = quantity;
    }
  }
  else if(e.target.matches(".add-cart")){
    $miniSpinner.classList.remove("hidden");
    $cartIcon.textContent = "";
    fetch("/cart/items",{
      method:"POST",
      headers:{"Content-Type":"application/json"},
      body:JSON.stringify({
        item:sessionStorage.getItem("id"),
        quantity:quantity
      })
    })
    .then((response)=>{
      if(response.status === 201){
        $cart.classList.remove("hidden");
      }
      else if(response.status === 401){
        window.location.href = "/login";
      }
    })
    .catch((error)=>{
      console.error(error);
    })
    .finally(()=>{
      $miniSpinner.classList.add("hidden");
      $cartIcon.textContent = "shopping_cart_checkout";
    });
  }
});