﻿namespace SILF.Script;


internal class Instance
{

    /// <summary>
    /// Consola de debug
    /// </summary>
    private IConsole? Console { get; set; }


    public Environments Environment { get; private set; }


    /// <summary>
    /// Lista de funciones.
    /// </summary>
    //private List<FuncionSILF> Functions = new();



    /// <summary>
    /// Lista de tipos
    /// </summary>
    public List<Tipo> Tipos = new()
    {
        new("string"),
        new("number"),
        new("precision"),
        new("bool"),
        new("char"),
        new("operator")
    };



    /// <summary>
    /// Nueva estancia de la app.
    /// </summary>
    /// <param name="console">IConsole</param>
    public Instance(IConsole? console, Environments environment)
    {
        this.Console = console;
        this.Environment = environment;
    }




    /// <summary>
    /// Escribe sobre la consola.
    /// </summary>
    /// <param name="result">Resultado.</param>
    public void Write(string result) => Console?.InsertLine(result, LogLevel.None);



    /// <summary>
    /// Escribe sobre la consola.
    /// </summary>
    /// <param name="result">Resultado.</param>
    public void WriteError(string result) => Console?.InsertLine(result, LogLevel.Error);



    /// <summary>
    /// Escribe sobre la consola.
    /// </summary>
    /// <param name="result">Resultado.</param>
    public void WriteWarning(string result) => Console?.InsertLine(result, LogLevel.Warning);


}