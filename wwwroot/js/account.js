const $body = document.querySelector("body");
const $delete = document.querySelector(".delete-profile");
const $purchaseList = document.querySelector(".purchase-list");

fetch("/account/profile")
.then(async(response)=>{
  const userData = await response.json();
  const $fullname = document.getElementById("fullname");
  const $email = document.getElementById("email");
  $fullname.textContent = `${userData.name} ${userData.lastName}`;
  $email.textContent = userData.email
})
.catch((error)=>{
  console.error(error);
});

fetch("/purchase")
.then(async(response)=>{
  if(response.status === 200){
    const purchases = await response.json();
    purchases.forEach(purchase => {
      const $purchaseDiv = document.createElement("li");
      const $idPurchaseOrder = document.createElement("p");
      const $purchaseDate = document.createElement("p");
      const $total = document.createElement("p");
      const date = new Date(purchase.datePurchase).toLocaleString();
      $purchaseDiv.setAttribute("class","purchase");
      $purchaseList.appendChild($purchaseDiv);
      $purchaseDiv.appendChild($idPurchaseOrder);
      $purchaseDiv.appendChild($purchaseDate);
      $idPurchaseOrder.textContent = `Orden de compra Nº: ${purchase.idPurchaseFormat}`;
      $purchaseDate.textContent = `Fecha: ${date}`;
      $total.setAttribute("class","total-purchase");
      purchase.purchaseDetail.forEach(purchaseDetail => {
        const $itemName = document.createElement("p");
        const $quantity = document.createElement("p");
        const $unitPrice = document.createElement("p");
        const $subtotal = document.createElement("p");
        $purchaseDiv.appendChild($itemName);
        $purchaseDiv.appendChild($quantity);
        $purchaseDiv.appendChild($unitPrice);
        $purchaseDiv.appendChild($subtotal);
        $itemName.textContent = `Articulo: ${purchaseDetail.itemName}`;
        $quantity.textContent = `Cantidad: ${purchaseDetail.quantity}`;
        $unitPrice.textContent = `Precio unitario: $${Number(purchaseDetail.itemPrice).toFixed(2)}`;
        $subtotal.textContent = `Subtotal: $${Number(purchaseDetail.subtotal).toFixed(2)}`;
      });
      $purchaseDiv.appendChild($total);
      $total.textContent = `Total: $${Number(purchase.total).toFixed(2)}`;
    });

  }
})
.catch((error)=>{
  console.log(error);
})
.finally(()=>{
  const $spinner = document.querySelector(".big-spinner");
  $spinner.classList.add("hidden");
});

$body.addEventListener("click",(e)=>{
  if(e.target.matches("#close-session")){
    fetch("/account/logout",{
      method:"POST"
    })
    .catch((error)=>{
      console.error(error);
    });
  }
});