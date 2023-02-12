using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PR;

public enum Tipo
{
    TIPO1,
    TIPO2,
    TIPO3
}

public class TesteScriptHideIf : MonoBehaviour
{
    public string nome;

    public Tipo tipo;

    [HideIf(nameof(tipo), false, Tipo.TIPO1)]
    public string algoquesofazsentidoprotipo1;

    [HideIf(nameof(tipo), false, Tipo.TIPO2)]
    public string algoquesofazsentidoprotipo2;

    [HideIf(nameof(tipo), false, Tipo.TIPO3)]
    public string algoquesofazsentidoprotipo3;
}
