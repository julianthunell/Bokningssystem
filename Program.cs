using System;

namespace Bokningssystem
{
    class Program
    {
        public static string[,] bussFält = new string[22, 4];
        public static double vinst = 0;
        public static Boolean system = true;
        static void Main(string[] args)
        {
            while (system) {
                meny();
            }
        }
        static void bokning()
        {
            Console.Clear();
            Console.WriteLine("Vill du boka fönster plats?");
            Console.WriteLine("1. ja");
            Console.WriteLine("2. nej");

            int val = Convert.ToInt32(Console.ReadLine());

            Boolean vanligPlats = false;
            if (val == 2)
                vanligPlats = true;
            //börjar söka igenom för ledig plats
            int plats = 0;
            Boolean adderaEtt = true;
            Console.Clear();
            int platsHittad = 0;
                while (plats < 21 && platsHittad == 0 )
                {
                    if (adderaEtt)
                    {
                        plats++;
                        if (bussFält[plats, 2] == null)
                            platsHittad = plats;
                        if (val != 2)    
                            adderaEtt = false;
                    }
                    else
                    {
                        if (plats == 17)
                        {
                            plats += 4;
                        }
                        else
                        {
                            plats += 3;
                        }
                        if (bussFält[plats, 2] == null)
                            platsHittad = plats;
                        adderaEtt = true;
                    }
                        
                }
                Console.WriteLine("Du har fått plats "+platsHittad);
                Console.WriteLine("Ange Namn (förnamn efternamn)");
                bussFält[plats, 0] = Console.ReadLine();
                Console.WriteLine("Ange personnummer");
                bussFält[plats, 1] = Console.ReadLine();
                Console.WriteLine("Ange kön");
                bussFält[plats, 2] = Console.ReadLine();
                int ålder = 2023 - Convert.ToInt32(bussFält[plats, 1].Substring(0, 4));
                bussFält[plats, 3] = ålder.ToString();
        }
        static void hittaBokning()
        {
            Console.Clear();
            Console.WriteLine("Ange personnummer eller namn för att hitta bokning(förnamn efternamn/YYYYMMDD");

            String input = Console.ReadLine();
            for (int i = 1; i <= 21; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (input == bussFält[i, j])
                    {
                        Console.WriteLine("Du har plats: " + i);
                        Console.WriteLine("Vill du avboka?");
                        Console.WriteLine("1. Ja");
                        Console.WriteLine("2. Nej");

                        String val = Console.ReadLine();
                        if (val == "1")
                        {
                            for (int h = 0; h < 3; h++)
                            {
                                bussFält[i, h] = null;
                            }
                        }
                    }
                }
            }

        }
        static void kollaLedigaPlatser()
        {
            int ledigaPlatser = 0;
            int bokadePlatser = 0;
            for (int i = 1; i < 22; i++)
            {
                if (bussFält[i, 1] == null)
                {
                    ledigaPlatser++;
                }
                else
                {
                    bokadePlatser++;
                }
            }
            Console.WriteLine("Antal lediga platser: "+ledigaPlatser);
            Console.WriteLine("Antal bokade platser: " + bokadePlatser);

        }
        static void beräknaVinsten(int varv)
        {
            if (varv>21)
            {
                Console.WriteLine("Beräknad vinst: "+vinst+"kr");
                return;
            }
            if (bussFält[varv, 1] != null)
            {
                if (2023 - Int32.Parse(bussFält[varv,3]) < 18)
                {
                    vinst+= 149.9;
                }
                else if (2023 - Int32.Parse(bussFält[varv, 3]) >= 69)
                {
                    vinst+= 299.9;
                }
                else
                {
                    vinst += 200.0;
                }
            }
            beräknaVinsten(varv + 1);
        }
        static void sorteraPassagerare()
        {
            Boolean swap = true;
            string[] tempFält = new string[4];
            while (swap)
            {
                swap = false;
                for (int i = 1; i < 22; i++)
                {
                    if (bussFält[i, 3] == null)
                    {
                        bussFält[i, 0] = 0.ToString();
                    }
                }
                for (int i = 1; i < 22 - 1; i++)
                {
                    int tal1 = Int32.Parse(bussFält[i, 3]);
                    int tal2 = Int32.Parse(bussFält[i+1, 3]);

                    if (tal1 < tal2)
                    {
                        swap = true;
                        for (int j = 0; j < 4; j++)
                        {
                            tempFält[j] = bussFält[i,j];
                        }
                        for (int j = 0; j < 4; j++)
                        {
                            bussFält[i, j] = bussFält[i + 1, j];
                        }
                        for (int j = 0; j < 4; j++)
                        {
                            bussFält[i + 1, j] = tempFält[j];
                        }
                    }
                }
            }
            for (int i = 1; i < 22; i++)
            {
                Console.WriteLine(bussFält[i,3]);
            }
        }
        static void meny(){
            Console.WriteLine("Vad vill du göra? 1-6");
            Console.WriteLine("1. Boka in person");
            Console.WriteLine("2. Kolla antal lediga platser");
            Console.WriteLine("3. Beräkna vinsten");
            Console.WriteLine("4. Hitta bokning/Avboka");
            Console.WriteLine("5. Sortera Passagerare efter ålder");
            Console.WriteLine("6. Avsluta");
            Console.WriteLine(" ");
            Boolean rätt = true;
            int val=0;

            while (rätt == true) {
                try
                {
                    val =Convert.ToInt32(Console.ReadLine());
                    rätt = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Du måste välja ett nummer mellan 1 och 6");
                    rätt = true;
                }
                rätt = false;
            }
            switch (val)
            {
                case 1:
                    bokning();
                    break;
                case 2:
                    kollaLedigaPlatser();
                    break;
                case 3:
                    vinst = 0;
                    beräknaVinsten(1);
                    break;
                case 4:
                    hittaBokning();
                    break;
                case 5:
                    sorteraPassagerare();
                    break;
                case 6:
                    Console.WriteLine("Nu avslutas programmet, om du har tyckt om det, snälla rösta oss 5 stjärnonr på google appstore och google play. Kom tillbaka nästa gång för ett spännande äventyr och en gratis bussresa på oss. 100 gratis flygpla´nsresos vart du vill + hotell.");
                    system = false;
                    break;
                default:
                    Console.WriteLine();
                    break;
                
            }
        }
    }
}