﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Grafos_TPP
{
    public class Program
    {
        /*
          Estrutura de representação - lista de adjacência
             Problema: Considere que você seja o responsável por elaborar o horário de aulas do curso de
                        Engenharia de Software da PUC Minas. O problema, nesse caso, consiste em alocar os
                        horários de cada turma do curso de forma a maximizar o número de turmas ofertadas em
                        paralelo. Para isso, é necessário respeitar a designação dos professores às suas
                        respectivas turmas. Considere também que alguns professores ministram aulas para
                        diversas turmas e que, por dia, possamos ter, no máximo, três horários de aula.

                        Além disso, lembre-se que turmas do mesmo período não podem ser alocadas no
                        mesmo horário; que uma turma só pode ser alocada a um professor; e que um mesmo
                        professor não pode lecionar para mais de uma turma em um dado horário.

            Resolução : coloração de vértices
            Vértices: materia/turma
            Arestas: conflitos(mesmo professor ou período)
        */

        public static Grafo g = new Grafo(new List<List<Vertice>>());

        public void LerArquivo() // Lê arquivo e cria vagas se nescessario.
        {
            if (File.Exists(@"C:\Users\Dell\Desktop\Grafos_TPP\Grafos_TPP\bin\Debug\netcoreapp2.1\a.txt"))
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Dell\Desktop\Grafos_TPP\Grafos_TPP\bin\Debug\netcoreapp2.1\a.txt"))
                {
                    List<Vertice> turmas = new List<Vertice>();
                    while (!reader.EndOfStream) // Enquanto arquivo não acaba.
                    {
                        string linha = reader.ReadLine();
                        string[] dados = linha.Split(';');
                        Vertice turma = new Vertice()
                        {
                            id = dados[0],
                            professor = dados[1],
                            materia = dados[2],
                            periodo = Int32.Parse(dados[3])
                        };
                        turmas.Add(turma); // le tudo e adiciona nesse vetor
                    }

                    // Montar o grafo - havera aresta entre dois pontos se eles tiverem o mesmo professor ou periodo


                    //primeiro adiconar no grafo todas as turmas(visão horizontal)
                    turmas.ForEach(turma =>
                    {
                        List<Vertice> lista = new List<Vertice>();
                        lista.Add(turma);
                        g.grafo.Add(lista);
                    });

                    //segundamente adicionar os conflitos(visao vertical - lisat de adj de cada turma/vertice

                    g.grafo.ForEach(turma =>
                    {
                        var conflitos = turmas.Where(t => t.materia != turma[0].materia && 
                        (t.professor == turma[0].professor || t.periodo == turma[0].periodo)).ToList();
                        conflitos.ForEach(conflito => turma.Add(conflito));
                    });


                }
            }
        }

        //------------------ Main
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.LerArquivo();

            Console.WriteLine("Cores na heuristica 1: ");
            g.Heuristica1();
            Console.WriteLine("Cores na heuristica 2: ");
            g.Heuristica2();
            Console.WriteLine("Cores na heuristica 3: ");
            g.Heuristica3();


        }
    }
}
