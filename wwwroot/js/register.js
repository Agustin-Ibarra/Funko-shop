const $body = document.querySelector("body");
const $name = document.getElementById("name");
const $lastName = document.getElementById("last-name");
const $email = document.getElementById("email");
const $password = document.getElementById("password");
const $confirmPassword = document.getElementById("conf-password");
const $error = document.querySelector(".sold-out");
const $spinner = document.querySelector(".spinner");

$body.addEventListener("click",(e)=>{
  if(e.target.matches(".send-form")){
    $error.classList.add("hidden");
    e.preventDefault();
    if($name.value && $lastName.value && $email.value && $password.value && $confirmPassword.value){
      if($password.value !== $confirmPassword.value){
        $error.textContent = "Las contraseñas no coinciden";
        $error.classList.remove("hidden");
      }
      else{
        $spinner.classList.remove("hidden");
        fetch("/register",{
          method:"POST",
          headers:{"Content-Type":"application/json"},
          body:JSON.stringify({
            name:$name.value,
            last_name:$lastName.value,
            email:$email.value,
            user_password:$password.value
          })
        })
        .then(async(response)=>{
          if(response.status === 201){
            window.location.href = "/login";
          }
          else{
            const error = await response.json();
            $error.textContent = error.error;
            $error.classList.remove("hidden");
          }
        })
        .catch((error)=>{
          console.error(error);
        })
        .finally(()=>{
          $spinner.classList.add("hidden");
        })
      }
    }
  }
});