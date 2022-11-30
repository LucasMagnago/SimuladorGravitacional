using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimuladorGravitacional.Models
{
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    internal class Corpo : CorpoAbs
    {
        private string? Nome;
        private double Massa;
        private double Densidade;
        private double PosicaoX;
        private double PosicaoY;
        private double VelocidadeX;
        private double VelocidadeY;
        
        public Corpo()
        {

        }
        public Corpo(string nome, double massa, double densidade, double posX, double posY, double velX, double velY)
        {
            Nome = nome;
            Massa = massa;
            Densidade = densidade;
            PosicaoX = posX;
            PosicaoY = posY;
            VelocidadeX = velX;
            VelocidadeY = velY;
        }

        public override string getNome()
        {
            return this.Nome;
        }
        public override double getMassa()
        {
            return this.Massa;
        }

        public override double getVelocidadeX()
        {
            return this.VelocidadeX;
        }

        public override double getVelocidadeY()
        {
            return this.VelocidadeY;
        }

        public override double getRaio()
        {
            double volume = this.Massa / this.Densidade;

            return Math.Pow(volume / ((4.0 / 3.0) * Math.PI), 1.0 / 3.0);
        }

        public override double getPosicaoX()
        {
            return this.PosicaoX;
        }

        public override double getPosicaoY()
        {
            return this.PosicaoY;
        }

        public override double getDensidade()
        {
            return this.Densidade;
        }

        public override double getForcaX()
        {
            //Implementar
            return 0;
        }

        public override double getForcaY()
        {
            //Implementar
            return 0;
        }

        public override void setVelocidadeX(double velX)
        {
            this.VelocidadeX = velX;
        }

        public override void setVelocidadeY(double velY)
        {
            this.VelocidadeY = velY;
        }

        public override void setPosicaoX(double x)
        {
            this.PosicaoX = x;
        }

        public override void setPosicaoY(double y)
        {
            this.PosicaoY = y;
        }

        public override void setDensidade(double densidade)
        {
            this.Densidade = densidade;
        }

        public override void setForcaX(double forcaX)
        {
            Console.WriteLine();
        }

        public override void setForcaY(double forcaY)
        {
            Console.WriteLine("");
        }   
    }
}
