# Pokemon Web API

  It is a pet-project that was made to get skills in developing REST APIs using ASP.NET Core.              
[Poke API](https://pokeapi.co) was used for requesting pokemons. It is open & free with hundreds of useful requests.                     
Except this some things were practiced too: EF Core & ASP.NET Core Identity for storing and managing users; Serilog;               
Authentication using JWT; Swagger for documenting API; Deploying on Azure - currently doesn't work, trial is ended ;(                  
  
  Previously server was working standalone, I mean without frontend and was succesfully tested by Postman.       
Later, i decided to study Angular, after few courses and reading Angular docs, I started developing frontend for my Pokemon Web API.


## Screenshots of working app
Angular server is running at https://localhost:4200, ASP.NET Core at https://localhost:5001 

This is main page, full list of pokemons is requested and got from [Poke API](https://pokeapi.co). It is scrollable.                              
For boosting proccess of writting frontend I was using [Angular Material UI](https://material.angular.io/) component library.
![MainPage](Screenshots/MainPage.png)

If user click on square started from picture till the end of page - he'll get pokemon-page. As we can see below, there are pokemon name, types, two sprites, 
height & weight.
![PokemonPage](Screenshots/PokemonPage.png)

Two more examples:                                                            
![Pokemon1](Screenshots/Pokemon1.png)
![Pokemon2](Screenshots/Pokemon2.png)


For registration user should click "Sign Up", that is located in the navbar and he'll navigated to registration-page.
![RegistarationPage](Screenshots/RegistarationPage.png)


There are validators for every field(form-control) of form.                                                                                       
Below we can see different validation message of different field validators.                                                               
![Validation](Screenshots/Validation.png)

When every field is correct button "Sign up" will be enaibled and user can click it.
![SuccesfulValidation](Screenshots/SuccesfulValidation.png)


If username or email is already existed in system, user will get error message:
![WrongRegistration](Screenshots/WrongRegistration.png)


If everything is correct after clicking "Sign up", user will get message and will be redirected in 5 seconds to login-page.
![SuccessfulRegistration](Screenshots/SuccessfulRegistration.png)

Authorization of users is implemented with JWT Bearer.Frontend client takes token from server with exp.date and validate it for every request to the server. 
Token is stored in client's localstorage.                                                                  
If user trying to login before confirmation of email - he'll get error.
![NotConfirmedEmailError](Screenshots/NotConfirmedEmailError.png)

So, user should check email and click link for email confirmation.
![EmailConfirmation](Screenshots/EmailConfirmation.png)

In PostgreSQL DB we can see row "EmailConfirmed" with false value, that means user's email isn't confirmed.
![PgAdminUnconfirmedEmailStatus](Screenshots/PgAdminUnconfirmedEmailStatus.png)

After click link in email message, it'll be true and login will be successful.
![PgAdminConfirmedEmailStatus](Screenshots/PgAdminConfirmedEmailStatus.png)

After signing in user will be navigated to dashboard-page.It shows pokemons that stored in DB.                                   
User can create, edit and delete them.
![SuccessfulLogin](Screenshots/SuccessfulLogin.png)

Token expiration date - 30 mins, and when it is over user'll be redirected to login-page and will get the message.
![SessionExpired](Screenshots/SessionExpired.png)

So, at dasboard-page user can click "Create" and he'll be redirected to create-pokemon-page.All fields have own validators,                                       Angular checking validity of everyone.Submiting button will be disabled until all fields will be valid.                                               
There is one more button - "Type".When user clicking it new fielnd is created => FormControl is pushed into FormArray.                                                                                 
![PokemonValidation](Screenshots/PokemonValidation.png)

Form without errors and enabled "Create" button.                                                              
                                                       ![ValidationPassed](Screenshots/ValidationPassed.png)

After creating some pokemons we can try to use filtering by types and searching by name.
![Dashboard](Screenshots/Dashboard.png)
![Find&Filter1](Screenshots/Find&Filter1.png)
![Find&FIlter2](Screenshots/Find&FIlter2.png)

If user click "Open" button, he'll navigated to edit-pokemon-page.
![EditingPokemon](Screenshots/EditingPokemon.png)

For demonstration I've added new type "Thermal" to Raichu
![EditedPokemon](Screenshots/EditedPokemon.png)

Also, there is Reset Password Feature.The mechanism such as with registration.                                                                                
User must input valid email into login-page and click "Forgot Password?", if email does't exist in DB user will get error message.
![ForgotPasswordValidation](Screenshots/ForgotPasswordValidation.png)

If all is OK, user will get message.
![SuccessfulForgotPasswrodValidation](Screenshots/SuccessfulForgotPasswrodValidation.png)

Then user should check email, click link and he'll be navigated to reset-password-page.
![ResetPasswordEmail](Screenshots/ResetPasswordEmail.png)

Thare are two parametrs in query string - UserId and Code. First param for searching user in DB, second for server validation JWT.               
![ResetPasswordPage](Screenshots/ResetPasswordPage.png)

After clicking button user will be redirected to login-page
There are no errors after request, so user's password is changed.                                                                                           
![NoReserPassErrors](Screenshots/NoReserPassErrors.png)
![NewPass](Screenshots/NewPass.png)

Thats all!







