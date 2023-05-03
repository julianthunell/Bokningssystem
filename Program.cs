using System;

namespace Bokningssystem
{
    class Program
    {
        public static string[,] bussFält = new string[22, 4]; //bussfält är ett tvidimensionellt fält som används för att lagra alla passagerares information
        public static double vinst = 0; //bara variabel för vinst.
        public static Boolean system = true; // system bestämmer om programmet ska vara igång eller inte
        static void Main(string[] args)
        {
            while (system) {
                meny(); //i main startas meny metoden
            }
        }
        //boknings algorithm
        //kollar först ifall du vill ha fönster plats eller ej.
        //kollar sedan igenom fältet och ser ifall en kollum i en rad är = null
        //ifall en kollumn i raden är null betyder det att raden är tom och platsen är ej bokad
        static void bokning()
        {
            Console.Clear();
            Console.WriteLine("Ifall du Vill boka fönster plats tryck: 1");
            Console.WriteLine("Ifall du inte vill boka fönster tryck valfri tangent.");
            Boolean rätt = true;
            int val = 0;
            while (rätt == true)
            {
                try
                {
                    val = Convert.ToInt32(Console.ReadLine());
                    rätt = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Du måste välja ett nummer mellan 1 och 2");
                    rätt = true;
                }
                rätt = false;
            }
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
                        if (val == 1)    
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
                Boolean rätt2 = true;
                while (rätt2)
                {
                try { 
                    Console.WriteLine("Ange personnummer");
                    bussFält[plats, 1] = Console.ReadLine();
                    int test = Convert.ToInt32(bussFält[plats, 1]);
                    test = test / 10000;
                    test = 2023-test;
                    if (test >=0 && test<1000)
                    {
                        rätt2 = false;
                    }
                    else
                    {
                        Console.WriteLine("Fel format på personnummer försök igen");
                        rätt2 = true;
                    }
                }
                catch(FormatException)
                {
                    Console.WriteLine("Fel format på personnummer försök igen");
                    rätt2 = true;
                }
                }
                Console.WriteLine("Ange kön");
                bussFält[plats, 2] = Console.ReadLine();
                int ålder = 2023 - Convert.ToInt32(bussFält[plats, 1].Substring(0, 4));
                bussFält[plats, 3] = ålder.ToString();
        }
        //algoritm för att hitta bokning och eventuellt avboka.
        // Det som händer är att den går dem första kollumnerna i varje rad och kollar ifall det är samma som det du har skrivit in.
        // Ifall din input stämmer in får du möjligheten att avboka
        static void hittaBokning()
        {
            Console.Clear();
            Console.WriteLine("Ange personnummer eller namn för att hitta bokning(förnamn efternamn/YYYYMMDD");
            Boolean platsHittad = false; //detta är för att kunna se sedan ifall du hittade platsen eller ej
            String input = Console.ReadLine();
            for (int i = 1; i <= 21; i++) // går igenom varje rad
            {
                for (int j = 0; j < 2; j++) //går igenom kollumn 1 och 2. Detta är alltså där person nummer och namnet står lagrat
                {
                    if (input == bussFält[i, j])
                    {
                        platsHittad = true;
                        Console.WriteLine("Du har plats: " + i);
                        Console.WriteLine("Vill du avboka?");
                        Console.WriteLine("1. Ja");
                        Console.WriteLine("2. Nej");

                        String val = Console.ReadLine();
                        if (val == "1") // ifall du väler att avboka så kommer den gå igenom hela din rad och tömma varje kollumn (sätta till null).
                        {
                            for (int h = 0; h < 3; h++)
                            {
                                bussFält[i, h] = null;
                            }
                        }
                    }
                }
            }
            if (platsHittad == false)
            {
                Console.WriteLine("Din plats hittades tyvärr inte");
            }

        }
        //kollar ifall det finns lediga platser
        static void kollaLedigaPlatser()
        {
            int ledigaPlatser = 0;
            int bokadePlatser = 0;
            // går igenom första kollumnen i varje rad och kollar ifall den är tom.
            // Ifall den är tom kommer ledigaplatser öka med 1.
            // ifall den är ifylld kommer innebär det att den platsen är bokad.
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
        //Denna metod är för att räkna vinst
        //varje gång metoden kallas så kollar den ifall varv > 21. Ifall detta är fallet kommer printa ut beräknad vinst och sedan sluta.
        //ifall varv inte är mer än 21 kommer metoden att forsätta kalla sig själv. Det som händer att varje gång metoden kallas räknar den ålder rutan i varje rad på bussfältet
        // sedan kollar programmet ålder och lägger till priset för den ålder i en "vinst" variabel.
        static void beräknaVinsten(int varv)
        {
            if (varv>21)
            {
                Console.WriteLine("Beräknad vinst: "+vinst+"kr");
                return;
            }
            if (bussFält[varv, 1] != null)
            {
                //ifall personen är under 18
                if (Int32.Parse(bussFält[varv,3]) < 18) 
                {
                    vinst += 149.90;
                }
                //ifall personen är >= 69
                else if (Int32.Parse(bussFält[varv, 3]) >= 69)
                {
                    vinst+= 200.0;
                }
                //ifall personen är vuxen
                else
                {
                    vinst += 299.90;
                }
            }
            beräknaVinsten(varv + 1);
        }
        //denna metod gör en kopia av bussfält och sedan sorterar passagerare efter ålder och printar ut en lista.
        //sorteringen sker via en bubble sort metod.
        static void sorteraPassagerare()
        {
            //först måste fältet kopieras
            string[,] bussFältKopia = new string[22, 5];
            for (int i = 1; i < 22; i++)
            {
                bussFältKopia[i, 0] = bussFält[i, 0]; //namn
                bussFältKopia[i, 1] = bussFält[i, 1]; // födelsedatum
                bussFältKopia[i, 2] = bussFält[i, 2]; //kön
                bussFältKopia[i, 3] = bussFält[i, 3]; //ålder
                bussFältKopia[i, 4] = i.ToString(); //detta blir sedan platsnunmmret

            }
            //sortering börjar här
            Boolean swap = true;
            string[] tempFält = new string[5];

            while (swap)
            {
                swap = false;
                //sätter alla de tomma rutorna till 0
                for (int i = 1; i < 22; i++)
                {
                    if (bussFältKopia[i, 3] == null)
                    {
                        bussFältKopia[i, 3] = "0";
                    }
                }
                //Bubblesort algoritmen
                for (int i = 1; i < 22 - 1; i++)
                {
                    //gör om string till int så man kan jämföra värden
                    int tal1 = Int32.Parse(bussFältKopia[i, 3]); 
                    int tal2 = Int32.Parse(bussFältKopia[i+1, 3]);

                    if (tal1 < tal2)
                    {
                        swap = true;
                        for (int j = 0; j < 5; j++)
                        {
                            tempFält[j] = bussFältKopia[i,j];
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            bussFältKopia[i, j] = bussFältKopia[i + 1, j];
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            bussFältKopia[i + 1, j] = tempFält[j];
                        }
                    }
                }
            }
            //printar ut själva listan
            for (int i = 1; i < 22; i++)
            {
                if (bussFältKopia[i,3]!="0")
                {
                    Console.WriteLine("Namn: " + bussFältKopia[i, 0] + " | " + "födelsedatum: " + bussFältKopia[i, 1] + " | " + "kön: " + bussFältKopia[i, 2] + " | " + "plats: " + bussFältKopia[i,4]);
                }
            }
        }
        //meny metod ganska självklart.
        //frågar vad användaren vill göra
        //sedan case switch för att kalla de olika metoderna beroende på val.
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
                    system = false;
                    break;
                default:
                    Console.WriteLine("Val måste vara mellan 1-6");
                    break;
                
            }
        }
    }
}