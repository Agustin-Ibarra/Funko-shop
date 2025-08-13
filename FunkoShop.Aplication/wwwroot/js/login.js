const $body = document.querySelector("body");
const $email = document.getElementById("email");
const $password = document.getElementById("password");
const $errorText = document.querySelector(".sold-out");
const $spinner = document.querySelector(".spinner");

$body.addEventListener("click",(e)=>{
  if(e.target.matches(".send-form")){
    e.preventDefault();
    $errorText.classList.add("hidden");
    if($email.value && $password.value){
      $spinner.classList.remove("hidden");
      fetch("/login",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body:JSON.stringify({email:$email.value,password:$password.value})
      })
      .then(async(response)=>{
        if(response.status >= 400){
          const error = await response.json();
          $errorText.classList.remove("hidden");
          $errorText.textContent = error.error;
        }
        else if(response.status === 200){
          window.location.href = "/";
        }
      })
      .catch((error)=>{
        console.error(error);
      })
      .finally(()=>{
        $spinner.classList.add("hidden");
      });
    }
  }
});