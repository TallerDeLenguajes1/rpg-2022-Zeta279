namespace JuegoRPG
{
    class Gameplay
    {
        public static float Ataque(Personaje atacante, Personaje defensor)
        {
            Random rnd = new Random();
            float poderDisparo;
            float efecDisparo;
            float valorAtaque;
            float poderDefensa;
            float maxDmgProvocable;
            float dmgProvocado;

            poderDisparo = atacante.Carac.Destreza * atacante.Carac.Fuerza * atacante.Carac.Nivel;
            efecDisparo = rnd.Next(1, 101);
            valorAtaque = poderDisparo * efecDisparo;
            poderDefensa = defensor.Carac.Armadura * defensor.Carac.Velocidad;
            maxDmgProvocable = 50000;
            dmgProvocado = (((valorAtaque * efecDisparo) - poderDefensa) / maxDmgProvocable) * 100;

            defensor.RecibirDMG(Convert.ToInt32(dmgProvocado));

            return dmgProvocado;
        }
    }
}
