const $body = document.querySelector('body');
const $listItems = document.querySelector('.items-list');
const $listChoice = document.querySelector('.list-options');
const $textOption = document.querySelector('.text-option');
const $input = document.querySelector("input");
const $searchText = document.querySelector(".search-text");
const $loadBtn = document.querySelector(".load-more");
const $loader = document.querySelector(".spinner-container");
const $loadBtnSpinner = document.querySelector(".load-more-spinner");
const $spinner = document.querySelector(".big-spinner");
let offset = 0;

const generateItem = function(items){
  items.forEach(item => {
    const $li = document.createElement("li");
    const $link = document.createElement("a");
    const $imgDiv = document.createElement("div");
    const $image = document.createElement("img");
    const $ItemData = document.createElement("div");
    const $category = document.createElement("p");
    const $name = document.createElement("p");
    const $price = document.createElement("p");
    $li.setAttribute("class","item");
    $li.appendChild($link);
    $li.appendChild($ItemData);
    $link.setAttribute("class","item-link");
    $link.setAttribute("href","/item");
    $link.appendChild($imgDiv);
    $imgDiv.appendChild($image);
    $image.setAttribute("src",`/images/${item.image}`);
    $image.setAttribute("id",item.idItem);
    $image.setAttribute("alt","item image");
    $ItemData.setAttribute("class","item-data");
    $ItemData.appendChild($category);
    $ItemData.appendChild($name);
    $ItemData.appendChild($price);
    $category.textContent = item.category;
    $category.setAttribute("class","category");
    $name.textContent = item.name;
    $name.setAttribute("class","item-name");
    $price.textContent = `$${Number(item.price).toFixed(2)}`;
    $price.setAttribute("class","price");
    $listItems.appendChild($li);
  });
}

const itemsDefault = function(){
  fetch(`/shop/items/${offset}`)
  .then(async(response)=>{
    const items = await response.json();
    offset += items.length;
    generateItem(items);
    if(items.length < 15){
      $loadBtn.classList.add("hidden");
    }
  })
  .catch((error)=>{
    console.error(error);
  })
  .finally(()=>{
    $loader.classList.add("hidden");
    $loadBtnSpinner.classList.add("hidden");
  });
}

const itemsByOrder = function(){
  fetch(`/shop/items/${sessionStorage.getItem("order")}/${offset}`)
  .then(async(response)=>{
    const items = await response.json();
    offset += items.length;
    generateItem(items);
    if(items.length < 15){
      $loadBtn.classList.add("hidden");
    }
  })
  .catch((error)=>{
    console.error(error);
  })
  .finally(()=>{
    $loader.classList.add("hidden");
    $loadBtnSpinner.classList.add("hidden");
  });
}

const itemsByFilter = function(){
  const $notFoundText = document.querySelector(".not-found");
  $notFoundText.textContent = "";
  $spinner.classList.remove("hidden");
  fetch(`/shop/items/filter/${sessionStorage.getItem("filter")}/${offset}`)
  .then(async(response)=>{
    if(response.status === 404){
      const error = await response.json();
      $notFoundText.textContent = error.error;
      $loader.classList.remove("hidden");
      $spinner.classList.add("hidden");
      $loadBtn.classList.add("hidden");
    }
    else if(response.status === 200){
      const items = await response.json();
      offset += items.length;
      generateItem(items);
      if(items.length < 15){
        $loadBtn.classList.add("hidden");
      }
      $loader.classList.add("hidden");
      $loadBtnSpinner.classList.add("hidden");
    }
  })
  .catch((error)=>{
    console.error(error);
  });
}

const itemsByFilterAndOrder = function(e){
  const $notFoundText = document.querySelector(".not-found");
  $notFoundText.textContent = "";
  $spinner.classList.remove("hidden");
  fetch(`/shop/items/filter/${sessionStorage.getItem("filter")}/${sessionStorage.getItem("order")}/${offset}`)
  .then(async(response)=>{
    if(response.status === 404){
      const error = await response.json();
      $notFoundText.textContent = error.error;
      $loader.classList.remove("hidden");
      $spinner.classList.add("hidden");
      $loadBtn.classList.add("hidden");
    }
    else if(response.status === 200){
      const items = await response.json();
      offset += items.length;
      generateItem(items);
      if(items.length < 15){
        $loadBtn.classList.add("hidden");
      }
      $loader.classList.add("hidden");
      $loadBtnSpinner.classList.add("hidden");      
    }
  })
  .catch((error)=>{
    console.error(error);
  });
}

if(sessionStorage.getItem("search")){
  if(sessionStorage.getItem("search") === "combined"){
    itemsByFilterAndOrder();
  }
  else if(sessionStorage.getItem("search") === "input"){
    itemsByFilter();
  }
  else{
    itemsByOrder();
  }
}
else{
  itemsDefault();
}

$body.addEventListener("click",(e)=>{
  if(e.target.matches(".load-more")){
    $loadBtnSpinner.classList.remove("hidden");
    if(sessionStorage.getItem("search")){
      if(sessionStorage.getItem("search") === "combined"){
        itemsByFilterAndOrder();
      }
      else if(sessionStorage.getItem("search") === "input"){
        itemsByFilter();
      }
      else if(sessionStorage.getItem("search") === "sort"){
        itemsByOrder();
      }
    }
    else{
      itemsDefault();
    }
  }
  else if(e.target.matches("img")){
    sessionStorage.setItem("id",e.target.id);
  }
  else if(e.target.matches(".select")){
    $listChoice.classList.toggle('hidden');
  }
  else if(e.target.matches("#desc")){
    $textOption.textContent = "Ordenar de menor a mayor precio";
    sessionStorage.setItem("text","Ordenar de menor a mayor precio");
    sessionStorage.setItem("order","asc");
    $textOption.classList.remove("hidden");
    $listChoice.classList.remove("hidden");
    $listChoice.classList.add('hidden');
  }
  else if(e.target.matches("#asc")){
    $textOption.textContent = "Ordenar de mayor a menor precio";
    sessionStorage.setItem("text","Ordenar de mayor a menor precio")
    sessionStorage.setItem("order","desc");
    $textOption.classList.remove("hidden");
    $listChoice.classList.remove("hidden");
    $listChoice.classList.add('hidden');
  }
  else if(e.target.matches(".search")){
    $loader.classList.remove("hidden");
    $loadBtn.classList.remove("hidden");
    if(sessionStorage.getItem("order") || $input.value || sessionStorage.getItem("filter")){
      const items = document.querySelectorAll(".item");
      items.forEach(item => {
        $listItems.removeChild(item);
      });      
      offset = 0;
      if(sessionStorage.getItem("order") && ($input.value || sessionStorage.getItem("filter"))){
        if($input.value){
          sessionStorage.setItem("filter",$input.value);
        }
        sessionStorage.setItem("search","combined");
        $searchText.textContent = `Filtrar por: ${sessionStorage.getItem("filter")}`;
        $searchText.classList.remove("hidden");
        itemsByFilterAndOrder();
      }
      else if(sessionStorage.getItem("order")){
        sessionStorage.setItem("search","sort");
        itemsByOrder();
      }
      else{
        sessionStorage.setItem("search","input");
        if($input.value){
          sessionStorage.setItem("filter",$input.value);
        }
        itemsByFilter();
        $searchText.textContent = `Filtrar por: ${sessionStorage.getItem("filter")}`;
        $searchText.classList.remove("hidden");
      }
    }
  }
  else if(e.target.matches(".clean")){
    if(sessionStorage.getItem("search")){
      sessionStorage.clear();
      window.location.reload();
    }
  }
});

window.addEventListener("load",()=>{
  if(sessionStorage.getItem("order")){
    $textOption.textContent = sessionStorage.getItem("text");
    $textOption.classList.remove("hidden");
  }
  if(sessionStorage.getItem("filter")){
    $searchText.textContent = `Filtrar por: ${sessionStorage.getItem("filter")}`;
    $searchText.classList.remove("hidden");
  }
});