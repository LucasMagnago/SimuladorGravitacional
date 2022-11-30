namespace SimuladorGravitacional.Models
{
    abstract class CorpoAbs
    {
        public abstract string getNome();
        public abstract double getMassa();
        public abstract double getVelocidadeX();
        public abstract double getVelocidadeY();
        public abstract double getRaio();
        public abstract double getPosicaoX();
        public abstract double getPosicaoY();
        public abstract double getDensidade();
        public abstract double getForcaX();
        public abstract double getForcaY();
        public abstract void setVelocidadeX(double velX);     
        public abstract void setVelocidadeY(double velY);
        public abstract void setPosicaoX(double posX);
        public abstract void setPosicaoY(double posY);
        public abstract void setDensidade(double densidade);
        public abstract void setForcaX(double forcaX);
        public abstract void setForcaY(double forcaY);
    }
}
