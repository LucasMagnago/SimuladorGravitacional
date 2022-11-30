namespace SimuladorGravitacional.Models
{
    internal class Universo : UniversoAbs
    {
        public List<Corpo> corpos = new List<Corpo>();
        private double gravidade = 6.674184 * (Math.Pow(10, -11));

        public override double CalcularForca(Corpo corpo1, Corpo corpo2)
        {
            double forca = 0.0;
            double distancia = CalcularDistancia(corpo1, corpo2)[2];

            forca = (gravidade * corpo1.getMassa() * corpo2.getMassa()) / (distancia * distancia);
            return forca;
        }

        public override double[] CalcularDistancia(Corpo corpo1, Corpo corpo2)
        {
            double[] distancias = new double[3];

            double distanciaHorizontal = corpo1.getPosicaoX() - corpo2.getPosicaoX();

            if (distanciaHorizontal < 0)
                distanciaHorizontal = distanciaHorizontal * (-1);

            double distanciaVertical = corpo1.getPosicaoY() - corpo2.getPosicaoY();

            if (distanciaVertical < 0)
                distanciaVertical = distanciaVertical * (-1);

            double hipotenusa = Math.Sqrt(Math.Pow(distanciaHorizontal, 2) + Math.Pow(distanciaVertical, 2));

            distancias[0] = distanciaHorizontal;
            distancias[1] = distanciaVertical;
            distancias[2] = hipotenusa;

            return distancias;
        }

        public override double DecomporForca(double forca, double[] distancias, string coordenada)
        {
            double forcaDecomposta = 0.0;
            double distanciaHorizontal = distancias[0];
            double distanciaVertical = distancias[1];
            double hipotenusa = distancias[2];

            if (coordenada == "x")
                forcaDecomposta = forca * (distanciaHorizontal / hipotenusa); //Fx = F * cos(a)  ->  Fx = F * (cateto adjascente / hipotenusa)
            else if (coordenada == "y")
                forcaDecomposta = forca * (distanciaVertical / hipotenusa); //Fy = F * sen(a)  ->  Fy = F * (cateto oposto / hipotenusa)

            return forcaDecomposta;
        }

        public override void IteracaoGravitacional(Corpo corpo1, Corpo corpo2)
        {
            throw new NotImplementedException();
        }

        public override bool VerificarColisao(Corpo corpo1, Corpo corpo2)
        {
            double[] distancias = CalcularDistancia(corpo1, corpo2);
            double hipotenusa = distancias[2];
            double raioC1 = corpo1.getRaio();
            double raioC2 = corpo2.getRaio();

            if(hipotenusa < (raioC1 + raioC2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
