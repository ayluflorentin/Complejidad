using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplejidadTPF
{
    public class Estrategia
    {
        public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
        {
            //  Cortamos si no hay datos
            if (clasificador == null || !clasificador.tieneDatos())
                return null;

            //  Cortamos si el clasificador no tiene atributos para seguir dividiendo
            if (clasificador.crearHoja())
            {
                var hoja = new DecisionData(clasificador.obtenerDatoHoja());
                return new ArbolBinario<DecisionData>(hoja);
            }

            // Creamos nodo con pregunta
            var pregunta = clasificador.obtenerPregunta();
            var nodo = new DecisionData(pregunta);
            var arbol = new ArbolBinario<DecisionData>(nodo);

            // Obtenemos clasificadores hijos
            Clasificador clasificadorIzq = clasificador.obtenerClasificadorIzquierdo();
            Clasificador clasificadorDer = clasificador.obtenerClasificadorDerecho();

            //  Validamos que no sean nulos
            if (clasificadorIzq == null || !clasificadorIzq.tieneDatos())
            {
                var hojaIzq = new DecisionData(clasificador.obtenerDatoHoja());
                arbol.setHijoIzquierdo(new ArbolBinario<DecisionData>(hojaIzq));
            }
            else
            {
                arbol.setHijoIzquierdo(CrearArbol(clasificadorIzq));
            }

            if (clasificadorDer == null || !clasificadorDer.tieneDatos())
            {
                var hojaDer = new DecisionData(clasificador.obtenerDatoHoja());
                arbol.setHijoDerecho(new ArbolBinario<DecisionData>(hojaDer));
            }
            else
            {
                arbol.setHijoDerecho(CrearArbol(clasificadorDer));
            }

            return arbol;
        }

        public string Consulta1(ArbolBinario<DecisionData> arbol)
        {
            List<string> predicciones = new List<string>();
            ObtenerPredicciones(arbol, predicciones);
            return string.Join("\n", predicciones);
        }

        private void ObtenerPredicciones(ArbolBinario<DecisionData> nodo, List<string> lista)
        {
            if (nodo == null) return;

            if (nodo.esHoja())
                lista.Add(nodo.getDato().ToString());
            else
            {
                ObtenerPredicciones(nodo.getHijoIzquierdo(), lista);
                ObtenerPredicciones(nodo.getHijoDerecho(), lista);
            }
        }

        public string Consulta2(ArbolBinario<DecisionData> arbol)
        {
            List<string> caminos = new List<string>();
            ObtenerCaminos(arbol, "", caminos);
            return string.Join("\n", caminos);
        }

        private void ObtenerCaminos(ArbolBinario<DecisionData> nodo, string camino, List<string> lista)
        {
            if (nodo == null) return;

            if (nodo.esHoja())
            {
                lista.Add(camino + "→ " + nodo.getDato().ToString());
            }
            else
            {
                string pregunta = nodo.getDato().ToString();
                ObtenerCaminos(nodo.getHijoIzquierdo(), camino + pregunta + " → Sí → ", lista);
                ObtenerCaminos(nodo.getHijoDerecho(), camino + pregunta + " → No → ", lista);
            }
        }

        public string Consulta3(ArbolBinario<DecisionData> arbol)
        {
            int altura = Altura(arbol);
            string resultado = "";

            for (int nivel = 0; nivel < altura; nivel++)
            {
                resultado += $"Nivel {nivel}:\n";
                List<string> datos = new List<string>();
                ObtenerNivel(arbol, nivel, datos);
                foreach (var dato in datos.Distinct())
                    resultado += "- " + dato + "\n";
            }

            return resultado;
        }

        private int Altura(ArbolBinario<DecisionData> nodo)
        {
            if (nodo == null) return 0;
            return Math.Max(Altura(nodo.getHijoIzquierdo()), Altura(nodo.getHijoDerecho())) + 1;
        }

        private void ObtenerNivel(ArbolBinario<DecisionData> nodo, int nivel, List<string> datos)
        {
            if (nodo == null) return;
            if (nivel == 0)
                datos.Add(nodo.getDato().ToString());
            else
            {
                ObtenerNivel(nodo.getHijoIzquierdo(), nivel - 1, datos);
                ObtenerNivel(nodo.getHijoDerecho(), nivel - 1, datos);
            }
        }
    }
}
