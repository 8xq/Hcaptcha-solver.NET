# Hcaptcha-solver
This is a very simple example of how to use "2captchaAPI" to solve "Hcaptcha"  , as you can see in the examples this "solving" task is broken into 2 parts to allow the user to do more whilst preparing the Hcaptcha token.

![Alt text](https://media.giphy.com/media/IYJoeBSab0TigyqEa1/giphy.gif "Example")



# Hcaptcha C# example (overview)
```
As you can see below there is only 3 parameters needed to solve "Hcaptcha"
To use this DLL simply import the DLL as a reference to the project 
Then follow the example below to setup your project <3
APIkey - (2captcha API key with balance)
SiteKey - specific to the site
Domain - This is the URL matching the "sitekey"
```
![Alt text](https://i.imgur.com/Fk1WnIt.png "Example")
# Hcaptcha C# usage
```
As you can see below there is only 2 methods needed to solve Hcaptcha 
As seen above 2captcha only requires 3 parameters to be provided
If captcha has any issues the bools returned will be false

Line 1 - Create object/instance for the solver class
Line 2 - Get a 2captcha task ID (starts process of 2captcha solving the hcaptcha)
Line 3 - This simply checks the 2captcha task ID if captcha was solved or not

Bool IDstatus (True/False) - checks if 2captcha accepted the request & if a task ID was returned
Bool CaptchaSolvedResult (True/False) - checks if captcha result was returned 
```
![Alt text](https://i.imgur.com/n5WjBtp.png "Example")





# Dependecies / Resources
[Leaf.xnet (web request)](https://github.com/csharp-leaf/Leaf.xNet "Leaf.xnet") <br>
[2captcha (API key)](https://2captcha.com?from=6752599 "2captcha.com")

```
Admin@hvh.site
```
