const body = document.querySelector("body");
const menu = document.querySelector("nav");
const options = document.querySelector(".options");
const optionsIcon = document.querySelector(".options-icon");

body.addEventListener("click",(e)=>{
  if(e.target.matches(".menu-btn") || e.target.matches(".burger")){
    menu.classList.add("show");
    optionsIcon.classList.remove("show");
    optionsIcon.classList.add("invisible");
  }
  else if(e.target.matches(".close")){
    menu.classList.remove("show");
    optionsIcon.classList.remove("invisible");
  }
  else if(e.target.matches(".options-icon")){
    console.log("options");
    options.classList.add("show");
    menu.classList.remove("show");
  }
  else if(e.target.matches(".icon-back") || e.target.matches(".search")){
    options.classList.remove("show");
  }
});