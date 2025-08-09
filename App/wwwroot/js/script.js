const body = document.querySelector("body");
body.addEventListener("click",(e)=>{
  if(e.target.matches(".menu-btn") || e.target.matches(".burger") || e.target.matches(".close-menu") || e.target.matches(".close")){
    const menu = document.querySelector("nav");
    menu.classList.toggle("show");
    console.log("menu");
  }
});