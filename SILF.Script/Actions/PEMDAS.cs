﻿namespace SILF.Script.Actions;


internal class PEMDAS
{


    /// <summary>
    /// Instancia actual de la app
    /// </summary>
    private Instance Instance { get; set; }


    /// <summary>
    /// Lista de valores
    /// </summary>
    private readonly List<Eval> Values = new();



    /// <summary>
    /// Nuevo solucionador PEMDAS
    /// </summary>
    /// <param name="instance">Instancia de la app</param>
    /// <param name="values">Valores a resolver</param>
    public PEMDAS(Instance instance, List<Eval> values)
    {
        this.Instance = instance;
        this.Values = values;
    }



    /// <summary>
    /// Resuelve las operaciones y concatenaciones de S# en orden.
    /// </summary>
    public void Solve()
    {
        
        SolveLL(); // Unarios
        //    A();  //Operadores aritmeticos
        //    N();  //Resuelve Nulos
        //    E();  //Exponentes
        SolveMD(); //Multiplicacion y Divicion
                   // P();  //%
        SolveSR(); //Adiccion y sustraccion
        //L_OY(); //Logico & y |
        //T(); // Terniarios
        
    }




    /// <summary>
    /// Soluciona multiplicaciones y divisiones.
    /// </summary>
    private void SolveMD()
    {
        // Operadores
        string[] operators = { "*", "/" };

        // Índice
        int index = Continue(operators, Values);

        // Si no hay indices
        if (index < 0)
            return;

        // Valores
        var (pre, ope, pos) = GetValues(index);


        if (pre == null || ope == null || pos == null)
        {
            Instance.WriteError("Error al realizar operaciones.");
            return;
        }

        // Valores finales
        object? value = null;
        Tipo type = new();

        // Operaciones
        if (pre.Tipo.Value.Description == "number" && pos.Tipo.Value.Description == "number")
        {
            // Si es preRun
            if (Instance.Environment == Environments.PreRun)
            {
                value = "0";
                type = pre.Tipo.Value;
            }

            // Valores
            else
            {
                // Valores numéricos
                bool canN1 = decimal.TryParse(pre.Value.ToString()?.Replace(".", ","), out var number1);
                bool canN2 = decimal.TryParse(pos.Value.ToString()?.Replace(".", ","), out var number2);

                // No se pudo convertir.
                if (!canN1 || !canN2)
                {
                    Instance.WriteError("Error de conversion numérica");
                    return;
                }

                // Segun el operador
                switch (ope.Value.ToString())
                {

                    // Multiplicación
                    case "*":
                        {
                            // Total
                            string total = (number1 * number2).ToString();

                            // Proceso
                            value = total;
                            type = pre.Tipo.Value;

                            break;
                        }

                    // División
                    case "/":
                        {

                            // Total
                            string total = (number1 / number2).ToString();

                            // Proceso
                            value = total;
                            type = pre.Tipo.Value;

                            break;
                        }

                }
            }

        }

        // Si el operador no es compatible
        else
        {
            // Error
            Instance.WriteError($"El operador '{ope.Value}' no es compatible para tipos <{pre.Tipo}> y <{pos.Tipo}>");

        }


        // Eliminar los valores

        Values.RemoveRange(index - 1, 3);
        Values.Insert(index - 1, new(value ?? "", type, false));



        SolveMD();

    }



    /// <summary>
    /// Soluciona sumas y restas.
    /// </summary>
    private void SolveSR()
    {
        // Operadores
        string[] operators = { "+", "-" };

        // Índice
        int index = Continue(operators, Values);

        // Si no hay indices
        if (index < 0)
            return;

        // Valores
        var (pre, ope, pos) = GetValues(index);

        if (pre == null || ope == null || pos == null)
        {
            Instance.WriteError("Error al realizar operaciones.");
            return;
        }

        // Valores finales
        object? value = null;
        Tipo type = new();

        // Operaciones
        if (pre.Tipo.Value.Description == "number" && pos.Tipo.Value.Description == "number")
        {
            // Si es preRun
            if (Instance.Environment == Environments.PreRun)
            {
                value = "0";
                type = pre.Tipo.Value;
            }

            // Valores
            else
            {
                // Valores numéricos
                bool canN1 = decimal.TryParse(pre.Value.ToString()?.Replace(".", ","), out decimal number1);
                bool canN2 = decimal.TryParse(pos.Value.ToString()?.Replace(".", ","), out decimal number2);

                // No se pudo convertir.
                if (!canN1 || !canN2)
                {
                    Instance.WriteError("Error de conversion numérica");
                    return;
                }

                // Segun el operador
                switch (ope.Value.ToString())
                {

                    // Suma
                    case "+":
                        {
                            // Total
                            string total = (number1 + number2).ToString();

                            // Proceso
                            value = total;
                            type = pre.Tipo.Value;

                            break;
                        }

                    // División
                    case "-":
                        {

                            // Total
                            string total = (number1 - number2).ToString();

                            // Proceso
                            value = total;
                            type = pre.Tipo.Value;

                            break;
                        }

                }
            }

        }

        // Concatenar
        else if ((pre.Tipo.Value.Description == "string" || pos.Tipo.Value.Description == "string") & ope.Value.ToString() == "+")
        {

            if (Instance.Environment == Environments.PreRun)
            {
                value = "";
                type = Instance.Tipos.Where(T => T.Description == "string").FirstOrDefault();
            }
            else
            {
                value = pre.Value.ToString() + pos.Value.ToString();
                type = Instance.Tipos.Where(T => T.Description == "string").FirstOrDefault();
            }

        }

        // Si el operador no es compatible
        else
        {
            // Error
            Instance.WriteError($"El operador '{ope.Value}' no es compatible para tipos <{pre.Tipo}> y <{pos.Tipo}>");

        }


        // Eliminar los valores
        try
        {
            Values.RemoveRange(index - 1, 3);
            Values.Insert(index - 1, new(value ?? "", type, false));
        }
        catch
        {

        }



        SolveSR();

    }



    /// <summary>
    /// Soluciona sumas y restas.
    /// </summary>
    private void SolveLL()
    {
        // Operadores
        string[] operators = { "!" };

        // Índice
        int index = Continue(operators, Values);

        // Si no hay indices
        if (index < 0)
            return;

        // Valores
        var (ope, pos) = GetTwoValues(index);

        if (pos == null || ope == null)
        {
            Instance.WriteError("Error al realizar operaciones.");
            return;
        }

        // Valores finales
        object? value = null;
        Tipo type = new();

        // Operaciones
        if (pos.Tipo.Value.Description == "bool")
        {


            // Segun el operador
            switch (ope.Value.ToString())
            {

                // Suma
                case "!":
                    {

                        if (pos.Value.ToString() == "false" || pos.Value.ToString() == "0")
                        {
                            value = "1";
                        }
                        else
                        {
                            value = "0";
                        }

                        type = pos.Tipo.Value;

                        break;
                    }

            }


        }

       

        // Si el operador no es compatible
        else
        {
            // Error
            Instance.WriteError($"El operador '{ope.Value}' no es compatible para tipos <{pos.Tipo}>");

        }


        // Eliminar los valores
        try
        {
            Values.RemoveRange(index , 2);
            Values.Insert(index , new(value ?? "", type, false));
        }
        catch
        {

        }



        SolveLL();

    }



    /// <summary>
    /// Comprueba si hay indices para ciertos operadores
    /// </summary>
    /// <param name="operators">Operadores a buscar</param>
    /// <param name="values">Valores</param>
    private static int Continue(string[] operators, List<Eval> values)
    {
        int index = values.FindIndex(T => T.Tipo.HasValue && T.Tipo.Value.Description == "operator" && operators.Contains(T.Value.ToString()));
        return index;
    }



    /// <summary>
    /// Obtiene los valores
    /// </summary>
    /// <param name="index">Índice</param>
    private (Eval? pre, Eval? ope, Eval? pos) GetValues(int index)
    {
        // Si no hay valores suficientes
        if (index <= 0 || index >= Values.Count)
        {
            Instance.WriteError("Error de operadores");
            return (null, null, null);
        }
        // Retorna elementos
        return (Values.ElementAtOrDefault(index - 1), Values.ElementAtOrDefault(index), Values.ElementAtOrDefault(index + 1));
    }



    /// <summary>
    /// Obtiene los valores
    /// </summary>
    /// <param name="index">Índice</param>
    private (Eval? pre, Eval? ope) GetTwoValues(int index)
    {
        // Si no hay valores suficientes
        if (index < 0 || index >= Values.Count)
        {
            Instance.WriteError("Error de operadores");
            return (null, null);
        }
        // Retorna elementos
        return (Values.ElementAtOrDefault(index ), Values.ElementAtOrDefault(index + 1));
    }



}