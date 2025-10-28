fetch(`/api/purchase/${sessionStorage.getItem("idPurchase")}`)
  .then(async (response) => {
    const $purchaseDiv = document.querySelector(".purchase-div");
    const purchase = await response.json();
    const $purchaseli = document.createElement("li");
    const $idPurchaseOrder = document.createElement("p");
    const $purchaseDate = document.createElement("p");
    const $total = document.createElement("p");
    const $subtotal = document.createElement("p");
    const date = new Date(purchase.datePurchase).toLocaleString();
    $purchaseli.setAttribute("class", "purchase");
    $purchaseDiv.appendChild($purchaseli);
    $purchaseli.appendChild($idPurchaseOrder);
    $purchaseli.appendChild($purchaseDate);
    $subtotal.setAttribute("class","subtotal-purchase");
    $idPurchaseOrder.textContent = `Orden de compra Nº: ${purchase.idPurchaseFormat}`;
    $purchaseDate.textContent = `Fecha: ${date}`;
    $total.setAttribute("class", "total-purchase");
    purchase.purchaseDetail.forEach(purchaseDetail => {
      const $itemName = document.createElement("p");
      const $quantity = document.createElement("p");
      const $unitPrice = document.createElement("p");
      $purchaseli.appendChild($itemName);
      $purchaseli.appendChild($quantity);
      $purchaseli.appendChild($unitPrice);
      $itemName.textContent = `Articulo: ${purchaseDetail.itemName}`;
      $quantity.textContent = `Cantidad: ${purchaseDetail.quantity}`;
      $unitPrice.textContent = `Precio unitario: $${Number(purchaseDetail.itemPrice).toFixed(2)}`;
      $subtotal.textContent = `Subtotal: $${Number(purchaseDetail.subtotal).toFixed(2)}`;
    });
    $purchaseli.appendChild($subtotal);
    $purchaseli.appendChild($total);
    $total.textContent = `Total: $${Number(purchase.total).toFixed(2)}`;
  })