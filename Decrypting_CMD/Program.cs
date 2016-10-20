using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Collections;
using System.Threading;

namespace Decrypting_CMD
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Origin obj = new Origin ();
			obj.origin ();
			//NamedPipeCliente cliente = new NamedPipeCliente ();

		}
	}

	public class Origin{
		/*
        * @param Variables Universales
        */
		static int TAM_MAX = 100;
		String[] user = new String[TAM_MAX];
		String[] paswordHash = new String[TAM_MAX];
		int cant = 0;
		Thread thread1;
		Thread thread2;
		Thread thread3;
		Thread thread4;
		Thread thread5;
		Thread thread6;
		Thread thread7;
		Thread thread8;

		/*
        * @param Metodo para administrar todos los procesos
        */
		public void origin(){
			read ();
			for(int i = 0; i<cant; i++)
			generatingDictionary (user[i],paswordHash[i], 8, 1);
		}

		/*
        * @param Metodo para crear diccionario de ataque
        */
		public String codeMD5(String bruteF){
			MD5 md5 = MD5CryptoServiceProvider.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = md5.ComputeHash(encoding.GetBytes(bruteF));
			for (int i = 0; i < stream.Length; i++) {
				sb.AppendFormat ("{0:x2}", stream [i]);
			}
			return sb.ToString();
		}

		/*
        * @param Metodo para romper hash
        */
		public int separateForce(String hashBruteForceMD5, String hash){
			String str1_FMD5 = null;		String str1_MD5 = null;
			String str2_FMD5 = null;		String str2_MD5 = null;
			String str3_FMD5 = null;		String str3_MD5 = null;
			String str4_FMD5 = null;		String str4_MD5 = null;
			int ver = 0;

			str1_FMD5 = hashBruteForceMD5.Substring(0,8);				str1_MD5 = hash.Substring(0,8);
			//Console.Write(str1_FMD5 + " * ");							Console.WriteLine(str1_MD5);
			str2_FMD5 = hashBruteForceMD5.Substring(8,8);				str2_MD5 = hash.Substring(8,8);
			//Console.Write(str2_FMD5 + " * ");							Console.WriteLine(str2_MD5);
			str3_FMD5 = hashBruteForceMD5.Substring(16,8);				str3_MD5 = hash.Substring(16,8);
			//Console.Write(str3_FMD5 + " * ");							Console.WriteLine(str3_MD5);
			str4_FMD5 = hashBruteForceMD5.Substring(24,8);				str4_MD5 = hash.Substring(24,8);
			//Console.Write(str4_FMD5 + " * ");							Console.WriteLine(str4_MD5);


			thread1 = new Thread (() => {
				ver = 0;
					//Console.WriteLine("Hilo 1 -> " + str1_FMD5 + " - " + str1_MD5);
				try	{
					ver = verifForce(str1_FMD5,str1_MD5);
					//Console.WriteLine(ver);
						if(ver == 1){
							thread1.Abort ();
							thread2.Abort ();
							thread3.Abort ();
							thread4.Abort ();
						}
					}catch (Exception e)
					{
						throw e;
					}
			} );

			thread2 = new Thread (() => {
				ver = 0;
				//Console.WriteLine("Hilo 1 -> " + str1_FMD5 + " - " + str1_MD5);
				try	{
					ver = verifForce(str1_FMD5,str1_MD5);
					//Console.WriteLine(ver);
					if(ver == 1){
						thread1.Abort ();
						thread2.Abort ();
						thread3.Abort ();
						thread4.Abort ();
					}
				}catch (Exception e)
				{
					throw e;
				}
			} );

			thread3 = new Thread (() => {
				ver = 0;
				//Console.WriteLine("Hilo 1 -> " + str1_FMD5 + " - " + str1_MD5);
				try	{
					ver = verifForce(str1_FMD5,str1_MD5);
					//Console.WriteLine(ver);
					if(ver == 1){
						thread1.Abort ();
						thread2.Abort ();
						thread3.Abort ();
						thread4.Abort ();
					}
				}catch (Exception e)
				{
					throw e;
				}
			} );

			thread4 = new Thread (() => {
				ver = 0;
				//Console.WriteLine("Hilo 1 -> " + str1_FMD5 + " - " + str1_MD5);
				try	{
					ver = verifForce(str1_FMD5,str1_MD5);
					//Console.WriteLine(ver);
					if(ver == 1){
						thread1.Abort ();
						thread2.Abort ();
						thread3.Abort ();
						thread4.Abort ();
					}
				}catch (Exception e)
				{
					throw e;
				}
			} );

			thread1.IsBackground = true;
			thread1.Start ();
			thread1.Join ();

			thread2.IsBackground = true;
			thread2.Start ();
			thread2.Join ();

			thread3.IsBackground = true;
			thread3.Start ();
			thread3.Join ();

			thread4.IsBackground = true;
			thread4.Start ();
			thread4.Join ();

			if (ver == 1) {
				return 1;
			}

			return 0;
		}

		/*
        * @param Metodo para romper hash
        */
		public int verifForce(String str_FMD5, String str_MD5){
			//Console.WriteLine (str_FMD5 + "--------- " + " ---------" + str_MD5);
			if (String.Compare(str_FMD5,str_MD5) == 0) {				
				//Console.WriteLine ("##############################################");
				//Console.ReadKey ();
				//thread1.Abort ();
				return 1;
			}
			return 0;
		}
		/*
        * @param Metodo para crear diccionario de ataque
        */
		public int generatingDictionary(String user, String hash, int tam, int min){
			char[] alpha = {'a','b','c','d','e','f','g','h','i',
							'j','k','l','m','n','ñ','o','p','q',
							'r','s','t','u','v','w','x','y','z',
							'A','B','C','D','E','F','G','H','I',
							'J','K','L','M','N','Ñ','O','P','Q',
							'R','S','T','U','V','W','X','Y','Z'};
			int tamAlpha = alpha.Length;
			String bruteF;
			String hashBruteForceMD5 = "";
			bruteF = "";

			for (int i = 0; i < tamAlpha; i++) {
				bruteF = "";
				bruteF = ("" + alpha[i]);
				//Console.WriteLine (bruteF);
				hashBruteForceMD5 = codeMD5(bruteF);
				if (separateForce (hashBruteForceMD5, hash) == 1) {
					Console.WriteLine ("--User: " + user);
					Console.WriteLine ("--Clave: " + bruteF);
					return 1;
				}
				//Console.WriteLine (hashBruteForceMD5);

				for (int j= 0; j < tamAlpha; j++) {
					bruteF = (alpha[i] + "" + alpha[j]);
					//Console.WriteLine (bruteF);
					hashBruteForceMD5 = codeMD5(bruteF);
					if (separateForce (hashBruteForceMD5, hash) == 1) {
						Console.WriteLine ("--User: " + user);
						Console.WriteLine ("--Clave: " + bruteF);
						return 1;
					}
					//Console.WriteLine (hashBruteForceMD5);

					for (int k = 0; k < tamAlpha; k++) {
						bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]);
						//Console.WriteLine (bruteF);
						hashBruteForceMD5 = codeMD5(bruteF);
						if (separateForce (hashBruteForceMD5, hash) == 1) {
							Console.WriteLine ("--User: " + user);
							Console.WriteLine ("--Clave: " + bruteF);
							return 1;
						}
						//Console.WriteLine (hashBruteForceMD5);
						//Console.ReadKey ();

						for (int l = 0; l < tamAlpha; l++) {
							Console.ReadKey ();
							bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]+ ""  + alpha[l]);
							//Console.WriteLine (bruteF);
							hashBruteForceMD5 = codeMD5(bruteF);
							if (separateForce (hashBruteForceMD5, hash) == 1) {
								Console.WriteLine ("--User: " + user);
								Console.WriteLine ("--Clave: " + bruteF);
								return 1;
							}
							//Console.WriteLine (hashBruteForceMD5);

							for (int m = 0; m < tamAlpha; m++) {
								bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]+ ""  + alpha[l]+ ""  + alpha[m]);
								//Console.WriteLine (bruteF);
								hashBruteForceMD5 = codeMD5(bruteF);
								if (separateForce (hashBruteForceMD5, hash) == 1) {
									Console.WriteLine ("--User: " + user);
									Console.WriteLine ("--Clave: " + bruteF);
									return 1;
								}
								//Console.WriteLine (hashBruteForceMD5);

								for (int n = 0; n < tamAlpha; n++) {
									bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n]);
									//Console.WriteLine (bruteF);
									hashBruteForceMD5 = codeMD5(bruteF);
									if (separateForce (hashBruteForceMD5, hash) == 1) {
										Console.WriteLine ("--User: " + user);
										Console.WriteLine ("--Clave: " + bruteF);
										return 1;
									}
									//Console.WriteLine (hashBruteForceMD5);

									for (int o = 0; o < tamAlpha; o++) {
										bruteF = (alpha[i] + "" + alpha[j] + "" + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n] + "" + alpha[o]);
										//Console.WriteLine (bruteF);
										hashBruteForceMD5 = codeMD5(bruteF);
										if (separateForce (hashBruteForceMD5, hash) == 1) {
											Console.WriteLine ("--User: " + user);
											Console.WriteLine ("--Clave: " + bruteF);
											return 1;
										}
										//Console.WriteLine (hashBruteForceMD5);

										for (int p = 0; p < tamAlpha; p++) {
											bruteF = (alpha[i] + "" + alpha[j] + "" + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n] + "" + alpha[o] + "" + alpha[p]);
											//Console.WriteLine (bruteF);
											hashBruteForceMD5 = codeMD5(bruteF);
											if (separateForce (hashBruteForceMD5, hash) == 1) {
												Console.WriteLine ("--User: " + user);
												Console.WriteLine ("--Clave: " + bruteF);
												return 1;
											}
											//Console.WriteLine (hashBruteForceMD5);
										}
									}
								}
							}
						}
					}
				}
			}

			return 0;
		}

		/*
        * @param Metodo para apertura y lectura de archivo
         * construcion de ArrayList
        */
		public void read()
		{
			string line;
			System.IO.StreamReader file = new System.IO.StreamReader(@"user.txt");
			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine ("Encrypting: " + line);
				breackString (line);
				cant++;
			}
			file.Close();
		}

		/*
        * @param Metodo para Romper Lineas
        */
		public void breackString(string line){
			char[] delimit = new char[] {':'};
			int cont = 0;
			foreach (String subStr in line.Split(delimit)) {
				if (cont == 2) {					
					Console.WriteLine("Password: " + subStr);
					paswordHash [cant] = subStr;

				} else {
					if (cont == 0) {
						Console.WriteLine ("User: " + subStr);
						user [cant] = subStr;
					}
				}
				cont++;
			}
			Console.WriteLine ();
		}


	}
}
