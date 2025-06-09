using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplejidadTPF
{
    // Clase Pregunta
    public class Pregunta
    {
        private string texto;

        public Pregunta(string texto)
        {
            this.texto = texto;
        }

        public string getTexto()
        {
            return this.texto;
        }

        public override string ToString()
        {
            return this.texto;
        }
    }

    // Clase DecisionData
    public class DecisionData
    {
        private Pregunta pregunta;
        private Dictionary<string, int> predicciones;

        public DecisionData(Pregunta pregunta)
        {
            this.pregunta = pregunta;
            this.predicciones = null;
        }

        public DecisionData(Dictionary<string, int> predicciones)
        {
            this.pregunta = null;
            this.predicciones = predicciones;
        }

        public bool esPregunta()
        {
            return pregunta != null;
        }

        public Pregunta getPregunta()
        {
            return this.pregunta;
        }

        public Dictionary<string, int> getPredicciones()
        {
            return this.predicciones;
        }

        public override string ToString()
        {
            if (esPregunta())
                return this.pregunta.ToString();

            string resultado = "";
            foreach (string etiqueta in predicciones.Keys)
            {
                resultado += etiqueta + ": " + predicciones[etiqueta] + " ";
            }
            return resultado.Trim();
        }
    }

    // Clase Clasificador
    public class Clasificador
    {
        private List<Dictionary<string, string>> datos;
        private string atributo;
        private string valorDivisor;
        private List<string> atributosDisponibles;
        private int nivel;

        public Clasificador(List<Dictionary<string, string>> datos, string atributo, string valorDivisor, int nivel = 0, List<string> atributosDisponibles = null)
        {
            this.datos = datos;
            this.atributo = atributo;
            this.valorDivisor = valorDivisor;
            this.nivel = nivel;

            if (atributosDisponibles == null)
            {
                this.atributosDisponibles = new List<string>();
                foreach (var key in datos[0].Keys)
                {
                    if (key != "clase")
                        this.atributosDisponibles.Add(key);
                }
            }
            else
            {
                this.atributosDisponibles = atributosDisponibles;
            }
        }

        public bool crearHoja()
        {
            
            if (datos == null || datos.Count == 0)
                return true;

            string clase = datos[0]["clase"];
            foreach (var fila in datos)
            {
                if (fila["clase"] != clase)
                    return false;
            }
            return true;
        }

        public Dictionary<string, int> obtenerDatoHoja()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            foreach (var fila in datos)
            {
                string clase = fila["clase"];
                if (!resultado.ContainsKey(clase))
                    resultado[clase] = 0;
                resultado[clase]++;
            }
            return resultado;
        }

        public Clasificador obtenerClasificadorIzquierdo()
        {
            var sub = datos.FindAll(f => f.ContainsKey(atributo) && f[atributo] == valorDivisor);
            return CrearSubClasificador(sub);
        }

        public Clasificador obtenerClasificadorDerecho()
        {
            var sub = datos.FindAll(f => f.ContainsKey(atributo) && f[atributo] != valorDivisor);
            return CrearSubClasificador(sub);
        }

        private Clasificador CrearSubClasificador(List<Dictionary<string, string>> subset)
        {
            if (subset == null || subset.Count == 0)
                return null;

            if (nivel + 1 >= atributosDisponibles.Count)
                return null; //  EVITA seguir clasificando sin atributos nuevos

            string nuevoAtributo = atributosDisponibles[nivel + 1];
            bool atributoValido = subset.TrueForAll(f => f.ContainsKey(nuevoAtributo));

            if (!atributoValido)
                return null;

            return new Clasificador(subset, nuevoAtributo, "si", nivel + 1, atributosDisponibles);
        }
        public Pregunta obtenerPregunta()
        {
            return new Pregunta($"¿{atributo}?");
        }
        public bool tieneDatos()
        {
            return datos != null && datos.Count > 0;
        }
    }

    // Árbol binario genérico
    public class ArbolBinario<T>
    {
        private T dato;
        private ArbolBinario<T> hijoIzquierdo;
        private ArbolBinario<T> hijoDerecho;

        public ArbolBinario(T dato)
        {
            this.dato = dato;
            this.hijoIzquierdo = null;
            this.hijoDerecho = null;
        }

        public T getDato()
        {
            return this.dato;
        }

        public void setDato(T dato)
        {
            this.dato = dato;
        }

        public ArbolBinario<T> getHijoIzquierdo()
        {
            return this.hijoIzquierdo;
        }

        public void setHijoIzquierdo(ArbolBinario<T> hijoIzquierdo)
        {
            this.hijoIzquierdo = hijoIzquierdo;
        }

        public ArbolBinario<T> getHijoDerecho()
        {
            return this.hijoDerecho;
        }

        public void setHijoDerecho(ArbolBinario<T> hijoDerecho)
        {
            this.hijoDerecho = hijoDerecho;
        }

        public bool esHoja()
        {
            return this.hijoIzquierdo == null && this.hijoDerecho == null;
        }
    }
}

