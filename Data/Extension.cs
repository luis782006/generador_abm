namespace Generador_ABM.Data
{

    public static class Extension
    {
        public static void Swap(this List<Clase> lista, Clase item1, Clase item2)
        {
            var aux = item1;
            int index2 = lista.IndexOf(item2);
            lista[lista.IndexOf(item1)] = item2;
            lista[index2] = aux;
        }
    }
}
