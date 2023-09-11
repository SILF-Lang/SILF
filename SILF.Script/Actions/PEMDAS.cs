﻿namespace SILF.Script.Actions;


internal class PEMDAS
{

    /// <summary>
    /// Instancia de la app.
    /// </summary>
    private Instance Instance;


    private List<Eval> Values = new();




    public PEMDAS(Instance instance, List<Eval> values)
    {
        this.Instance = instance;
        this.Values = values;
    }



    /// <summary>
    /// Resulve las operaciones y concatenaciones de S# en orden.
    /// </summary>
    public void Solve()
    {
        //    A();  //Operadores aritmeticos
        //    N();  //Resuelve Nulos
        //    E();  //Exponentes
        MD(); //Multiplicacion y Divicion
              // P();  //%
        AS(); //Adiccion y sustraccion
        //L_OY(); //Logico & y |
        //T(); // Terniarios
    }




    private void MD()
    {

        // Comprueba si hay este operador
        string[] operadores = { "*", "/" };
        var @continue = Contains(operadores);

        if (!@continue)
            return;

        // Obtiene la ubicación.
        int index = Values.FindIndex(T => T.Tipo.Description == "operator" && operadores.Contains(T.Value.ToString()));


        // Evals
        Eval pre = Values[index - 1];
        Eval ope = Values[index];
        Eval pos = Values[index + 1];


        string valor = "";
        Tipo finalType = new();


        // Suma
        if (pre.Tipo.Description == "number" && pos.Tipo.Description == "number")
        {
            pre.Value = pre.Value.ToString().Replace(".", ",");
            pos.Value = pos.Value.ToString().Replace(".", ",");


            switch (ope.Value.ToString())
            {
                case "*":
                    {

                        double vl1 = 0;
                        double vl2 = 0;

                        //Trata de convertir el valor
                        if (double.TryParse(pre.Value.ToString(), out vl1) && double.TryParse(pos.Value.ToString(), out vl2))
                        {
                            //Proceso
                            string total = (vl1 * vl2).ToString();
                            valor = total;
                            finalType = pre.Tipo;
                        }

                        // Si no se pudo convertir
                        else
                        {
                            valor = "";
                        }

                        break;
                    }


                case "/":
                    {

                        double vl1 = 0;
                        double vl2 = 0;

                        //Trata de convertir el valor
                        if (double.TryParse(pre.Value.ToString(), out vl1) && double.TryParse(pos.Value.ToString(), out vl2))
                        {
                            //Proceso
                            string total = (vl1 / vl2).ToString();
                            valor = total;
                            finalType = pre.Tipo;
                        }

                        // Si no se pudo convertir
                        else
                        {
                            valor = "";
                        }

                        break;
                    }


            }

        }


        //Si no fue compatible
        else
        {
            Instance.WriteError("Error de operador");
        }

        {
            Values.RemoveRange(index-1, 3);
            Values.Insert(index - 1, new(valor, finalType, false));
        }

        //Recursividas de la funcion
        MD();
    }



    private void AS()
    {

        // Comprueba si hay este operador
        string[] operadores = { "+", "-" };
        var @continue = Contains(operadores);

        if (!@continue)
            return;

        // Obtiene la ubicación.
        int index = Values.FindIndex(T => T.Tipo.Description == "operator" && operadores.Contains(T.Value.ToString()));


        // Evals
        Eval pre = Values[index - 1];
        Eval ope = Values[index];
        Eval pos = Values[index + 1];


        string valor = "";
        Tipo finalType = new();


        // Suma
        if (pre.Tipo.Description == "number" && pos.Tipo.Description == "number")
        {
            pre.Value = pre.Value.ToString().Replace(".", ",");
            pos.Value = pos.Value.ToString().Replace(".", ",");


            if (Instance.Environment == Environments.PreRun)
            {
                valor = "0";
                finalType = pre.Tipo;
            }
            else
            {
                switch (ope.Value.ToString())
                {
                    case "+":
                        {

                            double vl1 = 0;
                            double vl2 = 0;

                            //Trata de convertir el valor
                            if (double.TryParse(pre.Value.ToString(), out vl1) && double.TryParse(pos.Value.ToString(), out vl2))
                            {
                                //Proceso
                                string total = (vl1 + vl2).ToString();
                                valor = total;
                                finalType = pre.Tipo;
                            }

                            // Si no se pudo convertir
                            else
                            {
                                valor = "";
                            }

                            break;
                        }


                    case "-":
                        {

                            double vl1 = 0;
                            double vl2 = 0;

                            //Trata de convertir el valor
                            if (double.TryParse(pre.Value.ToString(), out vl1) && double.TryParse(pos.Value.ToString(), out vl2))
                            {
                                //Proceso
                                string total = (vl1 - vl2).ToString();
                                valor = total;
                                finalType = pre.Tipo;
                            }

                            // Si no se pudo convertir
                            else
                            {
                                valor = "";
                            }

                            break;
                        }


                }
            }



        }


        // Concatenación
        else if ((pre.Tipo.Description == "string" || pos.Tipo.Description == "string") & ope.Value.ToString() == "+")
        {

            if (Instance.Environment == Environments.PreRun)
            {
                valor = "";
                finalType = Instance.Tipos.Where(T => T.Description == "string").FirstOrDefault();
            }
            else
            {
                valor = pre.Value.ToString() + pos.Value.ToString();
                finalType = Instance.Tipos.Where(T => T.Description == "string").FirstOrDefault();
            }


        }

        //Si no fue compatible
        else
        {
            Instance.WriteError("Error de operador");
        }

        {
            Values.RemoveRange(index - 1, 3);
            Values.Insert(index - 1, new(valor, finalType, false));
        }

        //Recursividas de la funcion
        AS();
    }









    private bool Contains(string[] operators)
    {
        var have = Values.Where(T => T.Tipo.Description == "operator" && operators.Contains(T.Value)).Any();
        return have;
    }








}