using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        //Este es el navegador Edge de microsoft "new EdgeDriver"  Puedees cambiarlo a el que prefieras
        IWebDriver driver = new EdgeDriver();
        //Este es el url, en este caso solo funcionaria con IG porque es de donde tome las direciones de XPath para llamar los elementos
        driver.Navigate().GoToUrl("https://www.instagram.com/");

        // Esperar 10 segundos para que cargue la pagina
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        // Ingresar nombre de usuario y contraseña
        IWebElement usernameField = driver.FindElement(By.Name("username"));
        IWebElement passwordField = driver.FindElement(By.Name("password"));
        //Aqui va el usuario y contraseña del perfil al que quieres que ingrese
        usernameField.SendKeys("usuario");
        passwordField.SendKeys("clave");
        passwordField.Submit();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //Hacer click en la lupa
        driver.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(10);
        driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div[2]/div/a/div")).Click();


        //Escribier en la barra de la lupa de busqueda
        driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div[2]/div/div/div[2]/div[1]/div/input")).SendKeys("kimkardashian");

        //HAcer click en el perfil que aparezca de primero
        driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div[2]/div/div/div[2]/div[2]/div/div[1]/div/a/div")).Click();


        //espera
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

        //hacer click en los seguidores dentro de ese perfil
        driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[2]/a/div")).Click();
        
        //Con esto limitaremos las personas a seguir a solo 50, porque si sigues mas de 200 en un dia IG lo interpreta como actividad sospechosa
        int botonn = 100;

        //Con este bucle repetiremos la accion de seguir las 5 personas
        int contador = 1;
        for (int i = 1; i <= botonn; i++)
        {
            //Esta pausa es necesaria para pareser una interaccion Humana
            Thread.Sleep(4000);

            //Este es el XPATH del boton de seguir, el cual simplemente aumente un numero a medida que aparece otro boton se "Seguir"
            string xpath1 = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/div[2]/div/div[" + i + "]/div[3]/button";
            //Este es para obtener el nombre de un IG
            string xpath2 = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/div[2]/div/div[" + i + "]";

            //La variable boton que usaremos para hacer click en el boton mas adelante
            IWebElement boton = driver.FindElement(By.XPath(xpath1));
            IWebElement nombre = driver.FindElement(By.XPath(xpath2));

            //Encontrar las personas que ya sigues o que ya les enviaste una solicitud y su cuenta esta privada, eso evita que se trave el programa
            if (boton.Text == "Seguir")
            {
                //Aqui hace click a sguir si encontro el boton "Seguir"
                boton.Click();
                string nom = nombre.GetAttribute("aria-label");
                //Aqui escribe en pantalla que siguio a alguien
                Console.WriteLine("Se siguio a la persona #" + contador + ": " + nom);
                contador++;
            }
        }
        //Termina de seguir a las 50 personas y escribe que lo hizo
        Console.WriteLine("WE DID ITTTTT");

        driver.Quit();
    }
}
//CREDITOS:KALI2911