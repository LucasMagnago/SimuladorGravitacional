namespace SimuladorGravitacional.Models
{
    abstract class UniversoAbs
    {
        public abstract double CalcularForca(Corpo corpo1, Corpo corpo2);
        public abstract void IteracaoGravitacional(Corpo corpo1, Corpo corpo2);
        public abstract double[] CalcularDistancia(Corpo corpo1, Corpo corpo2);
        public abstract double DecomporForca(double forca, double[] distancias, string coordenada);
        public abstract bool VerificarColisao(Corpo corpo1, Corpo corpo2);
    }
}
