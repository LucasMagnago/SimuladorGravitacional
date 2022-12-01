using Microsoft.AspNetCore.Mvc;
using SimuladorGravitacional.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.Json;

namespace SimuladorGravitacional.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return Reset();
        }

        public IActionResult Start()
        {
            try
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string path = contentRootPath + @"\\Files\\entrada.uni";

                string[] conteudo = new string[System.IO.File.ReadLines(path).Count()];
                conteudo = System.IO.File.ReadAllLines(path);

                int interacoes = 0;
                double tempo = 0;
                bool primeiraLinha = true;
                List<Corpo> corpos = new List<Corpo>();
                int quantidadeCorpos = 0;

                //Teste
                List<Corpo> listAux = new List<Corpo>();

                //LENDO E ALIMENTANDO MINHA LISTA
                foreach (string linha in conteudo)
                {
                    if (primeiraLinha)
                    {
                        if (!string.IsNullOrEmpty(linha))
                        {
                            string[] separacao = linha.Split(";");
                            quantidadeCorpos = int.Parse(separacao[0]);
                            interacoes = int.Parse(separacao[1]);
                            tempo = double.Parse(separacao[2]);
                            primeiraLinha = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(linha))
                        {
                            string[] separacao = linha.Split(";");

                            string nome = separacao[0];
                            double massa = double.Parse(separacao[1]);
                            double densidade = double.Parse(separacao[2]);
                            double posicaoX = double.Parse(separacao[3]);
                            double posicaoY = double.Parse(separacao[4]);
                            double velocidadeX = double.Parse(separacao[5]);
                            double velocidadeY = double.Parse(separacao[6]);

                            Corpo c = new Corpo(nome, massa, densidade, posicaoX, posicaoY, velocidadeX, velocidadeY);
                            corpos.Add(c);
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(quantidadeCorpos + ";" + interacoes);

                //Teste
                listAux = corpos;

                if (corpos.Count() == quantidadeCorpos)
                {
                    List<Corpo> listaCorpos = new List<Corpo>();
                    Universo universo = new Universo();

                    {/*INTERAÇÃO*/}
                    for (int numIteracao = 0; numIteracao < interacoes; numIteracao++)
                    {
                        sb.AppendLine("** Interacao " + numIteracao + "************");

                        //if (numIteracao == 0)
                        //{
                        //    listaCorpos = corpos;
                        //}

                        listaCorpos = corpos;
                        
                        for (int i = 0; i < quantidadeCorpos; i++)
                        {
                            double somaPosicaoX = 0.0;
                            double somaPosicaoY = 0.0;
                            double somaVelocidadeX = 0.0;
                            double somaVelocidadeY = 0.0;
                            for (int corpoCount = 0; corpoCount < quantidadeCorpos; corpoCount++)
                            {
                                double forcaX = 0.0;
                                double forcaY = 0.0;
                                if (corpoCount != i)
                                {
                                    {/*CHAMADA DOS MÉTODOS*/}
                                    double forca = universo.CalcularForca(
                                        listaCorpos[i],
                                        listaCorpos[corpoCount]);

                                    forcaX += universo.DecomporForca(
                                        forca,
                                        universo.CalcularDistancia(listaCorpos[i], listaCorpos[corpoCount]),
                                        "x");

                                    forcaY += universo.DecomporForca(
                                        forca,
                                        universo.CalcularDistancia(listaCorpos[i], listaCorpos[corpoCount]),
                                        "y");

                                    {/*CALCULO DA FORÇA TOTAL*/}
                                    double forcaTotal = Math.Pow(((forcaX * forcaX) + (forcaY * forcaY)), 0.5);

                                    double aceleracao = forcaTotal / listaCorpos[i].getMassa();

                                    double posicaoX = listaCorpos[i].getPosicaoX() +
                                    (listaCorpos[i].getVelocidadeX() * tempo) + ((aceleracao  * Math.Pow(tempo, 2)) / 2);

                                    double posicaoY = listaCorpos[i].getPosicaoY() +
                                    (listaCorpos[i].getVelocidadeY() * tempo) + ((aceleracao * Math.Pow(tempo, 2)) / 2);

                                    double velocidadeX = listaCorpos[i].getVelocidadeX() +
                                    aceleracao * tempo;

                                    double velocidadeY = listaCorpos[i].getVelocidadeY() +
                                     aceleracao * tempo;

                                    {
                                        if (somaPosicaoX == 0)
                                            somaPosicaoX = corpos[i].getPosicaoX();
                                        if (somaPosicaoY == 0)
                                            somaPosicaoY = corpos[i].getPosicaoY();
                                        if (somaVelocidadeX == 0)
                                            somaVelocidadeX = corpos[i].getVelocidadeX();
                                        if (somaVelocidadeY == 0)
                                            somaVelocidadeY = corpos[i].getVelocidadeY();
                                    }

                                    if(listaCorpos[i].getPosicaoX() < listaCorpos[corpoCount].getPosicaoX())
                                    {
                                        somaPosicaoX += posicaoX - listaCorpos[i].getPosicaoX();
                                        somaVelocidadeX += velocidadeX - listaCorpos[i].getVelocidadeX() ;
                                    }
                                    else
                                    {
                                        somaPosicaoX -= posicaoX - listaCorpos[i].getPosicaoX();
                                        somaVelocidadeX -= velocidadeX - listaCorpos[i].getVelocidadeX();
                                    }

                                    if (listaCorpos[i].getPosicaoY() < listaCorpos[corpoCount].getPosicaoY())
                                    {
                                        somaPosicaoY += posicaoY - listaCorpos[i].getPosicaoY();
                                        somaVelocidadeY += velocidadeY - listaCorpos[i].getVelocidadeY();
                                    }
                                    else
                                    {
                                        somaPosicaoY -= posicaoY - listaCorpos[i].getPosicaoY();
                                        somaVelocidadeY -= velocidadeY - listaCorpos[i].getVelocidadeY();
                                    }

                                    //somaPosicaoX = somaPosicaoX + posicaoX - listaCorpos[i].getPosicaoX();
                                    //somaPosicaoY = somaPosicaoY + posicaoY - listaCorpos[i].getPosicaoY();
                                    //if (posicaoX < listaCorpos[i].getPosicaoX())
                                    //{
                                    //    somaVelocidadeX -= velocidadeX +
                                    //    listaCorpos[i].getVelocidadeX();
                                    //}
                                    //else
                                    //{
                                    //    somaVelocidadeX += velocidadeX +
                                    //    listaCorpos[i].getVelocidadeX();
                                    //}
                                    //if (posicaoY < listaCorpos[i].getPosicaoY())
                                    //{
                                    //    somaVelocidadeY -= velocidadeY +
                                    //    listaCorpos[i].getVelocidadeY();
                                    //}
                                    //else
                                    //{
                                    //    somaVelocidadeY += velocidadeY +
                                    //    listaCorpos[i].getVelocidadeY();
                                    //}
 
                                }
                            }

                            string nome = corpos[i].getNome();
                            double massa = corpos[i].getMassa();
                            double densidade = corpos[i].getDensidade();
                            double pX = somaPosicaoX;
                            double pY = somaPosicaoY;
                            double vX = somaVelocidadeX;
                            double vY = somaVelocidadeY;

                            Corpo c = new Corpo(nome, massa, densidade, pX, pY, vX, vY);
                            corpos[i] = c;

                            sb.AppendLine(c.getNome() + ";" + c.getMassa() + ";" + c.getPosicaoX() + ";" + c.getPosicaoY() + ";" + c.getVelocidadeX() + ";" + c.getVelocidadeY());

                            // ADICIONA ITENS A LISTA
                            //listaCorpos.Add(c);

                            //Teste
                            listAux.Add(c);
                        }

                        //for (int j = 0; j < quantidadeCorpos; j++)
                        //{
                        //    for (int corpoCount = 0; corpoCount < quantidadeCorpos; corpoCount++)
                        //    {
                        //        if (corpoCount != j)
                        //        {
                        //            bool colisao = universo.VerificarColisao(listaCorpos[j], listaCorpos[corpoCount]);

                        //            if (colisao)
                        //            {
                        //                if (listaCorpos[quantidadeCorpos + j].getMassa() > listaCorpos[quantidadeCorpos + corpoCount].getMassa())
                        //                {
                        //                    Corpo corpoRemover = listaCorpos[quantidadeCorpos + corpoCount];

                        //                    corpos.Remove(corpoRemover);
                        //                    listaCorpos.Remove(corpoRemover);
                        //                }
                        //                else
                        //                {
                        //                    Corpo corpoRemover = listaCorpos[quantidadeCorpos + j];

                        //                    corpos.Remove(corpoRemover);
                        //                    listaCorpos.Remove(corpoRemover);
                        //                }

                        //                quantidadeCorpos--;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    throw new Exception("Número de corpos incorreto no arquivo !!");
                }

                StreamWriter sw = new StreamWriter(contentRootPath + @"\\Files\\saida.txt");
                sw.Write(sb.ToString());
                sw.Close();

                ViewData["nElementos"] = quantidadeCorpos;
                ViewData["nCiclos"] = interacoes;
                ViewData["Start"] = true;
                return View("Index", listAux);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ocorreu um erro na geração do arquivo: ${e.Message}");

                ViewData["ErrorMessage"] = $"Ocorreu um erro na geração do arquivo: ${e.Message}";
                ViewData["hasError"] = true;
                ViewData["Start"] = false;
                return View("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Algo deu errado: ${e.Message}");

                ViewData["ErrorMessage"] = $"Algo deu errado: ${e.Message}";
                ViewData["hasError"] = true;
                ViewData["Start"] = false;
                return View("Index");
            }
             
        }

        public IActionResult Reset()
        {
            ViewData["nElementos"] = 0;
            ViewData["nCiclos"] = 0;
            ViewData["Start"] = false;
            return View("Index");
        }
    }
}