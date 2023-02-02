#nullable enable

internal interface IEnhanceWithMado<T1>
    where T1 : Mado
{
    T1 MadoEnhancer { get; set; }
}

internal interface IEnhanceWithMado<T1, T2>
    where T1 : Mado
    where T2 : Mado?

{
    T1 MadoEnhancer1 { get; set; }
    T2 MadoEnhancer2 { get; set; }
}

internal interface IEnhanceWithMado<T1, T2, T3>
    where T1 : Mado
    where T2 : Mado
    where T3 : Mado
{
    T1 MadoEnhancer1 { get; set; }
    T2 MadoEnhancer2 { get; set; }
    T3 MadoEnhancer3 { get; set; }
}

internal interface IEnhanceWithMado<T1, T2, T3, T4>
    where T1 : Mado
    where T2 : Mado
    where T3 : Mado
    where T4 : Mado
{
    T1 MadoEnhancer1 { get; set; }
    T2 MadoEnhancer2 { get; set; }
    T3 MadoEnhancer3 { get; set; }
    T4 MadoEnhancer4 { get; set; }
}