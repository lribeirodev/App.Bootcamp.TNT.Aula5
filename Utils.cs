using System.Collections;

public static class Utils<T>{

    public static string[] gerarArrayString(ArrayList a){
        int cont = 0;
        string[] lista = new string[a.Count];
        foreach(T item in a){
            lista[cont] = item.ToString()!;
            cont++;
        }
        return lista;
    }
}