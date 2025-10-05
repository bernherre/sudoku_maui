using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Engine;

public sealed class SudokuEngine
{
    public int N { get; }
    public int BoxRows { get; }
    public int BoxCols { get; }
    private int _seed;

    public SudokuEngine(int n = 6, int? seed = null)
    {
        if (n != 4 && n != 6) throw new ArgumentException("Solo 4x4 o 6x6", nameof(n));
        N = n;
        (BoxRows, BoxCols) = n == 4 ? (2, 2) : (3, 2);
        _seed = seed ?? Environment.TickCount;
    }

    private double Rng01()
    {
        _seed = (int)(((long)_seed * 48271) % 2147483647);
        return (double)_seed / 2147483647d;
    }

    private List<T> Shuffle<T>(IEnumerable<T> src)
    {
        var a = src.ToList();
        for (int i = a.Count - 1; i > 0; i--)
        {
            int j = (int)Math.Floor(Rng01() * (i + 1));
            (a[i], a[j]) = (a[j], a[i]);
        }
        return a;
    }

    public int[][] Empty() => Enumerable.Range(0, N).Select(_ => new int[N]).ToArray();
    public int[][] Clone(int[][] g) => g.Select(r => r.ToArray()).ToArray();

    public bool IsValid(int[][] g, int r, int c, int v)
    {
        if (v < 1 || v > N) return false;
        for (int i = 0; i < N; i++)
        {
            if (g[r][i] == v) return false;
            if (g[i][c] == v) return false;
        }
        int sr = (r / BoxRows) * BoxRows, sc = (c / BoxCols) * BoxCols;
        for (int rr = 0; rr < BoxRows; rr++)
            for (int cc = 0; cc < BoxCols; cc++)
                if (g[sr + rr][sc + cc] == v) return false;
        return true;
    }

    private (int r, int c)? FindEmpty(int[][] g)
    {
        for (int r = 0; r < N; r++)
            for (int c = 0; c < N; c++)
                if (g[r][c] == 0) return (r, c);
        return null;
    }

    public bool Solve(int[][] g)
    {
        var pos = FindEmpty(g);
        if (pos is null) return true;
        var (r, c) = pos.Value;
        foreach (var v in Shuffle(Enumerable.Range(1, N)))
        {
            if (IsValid(g, r, c, v))
            {
                g[r][c] = v;
                if (Solve(g)) return true;
                g[r][c] = 0;
            }
        }
        return false;
    }

    public int CountSolutions(int[][] g, int limit = 2)
    {
        int cnt = 0;
        void Dfs()
        {
            if (cnt >= limit) return;
            var pos = FindEmpty(g);
            if (pos is null) { cnt++; return; }
            var (r, c) = pos.Value;
            for (int v = 1; v <= N && cnt < limit; v++)
            {
                if (IsValid(g, r, c, v))
                {
                    g[r][c] = v; Dfs(); g[r][c] = 0;
                }
            }
        }
        Dfs();
        return cnt;
    }

    public (int[][] puzzle, int[][] solved) GeneratePuzzle(int clues)
    {
        var solved = Empty(); Solve(solved);
        var puzzle = Clone(solved);

        var idxs = Enumerable.Range(0, N * N).ToList();
        foreach (var idx in Shuffle(idxs))
        {
            if (puzzle.SelectMany(x => x).Count(x => x != 0) <= clues) break;
            int r = idx / N, c = idx % N;
            int bak = puzzle[r][c];
            if (bak == 0) continue;

            int rowFilled = puzzle[r].Count(x => x != 0);
            int colFilled = Enumerable.Range(0, N).Select(i => puzzle[i][c]).Count(x => x != 0);
            if (rowFilled <= N / 2 || colFilled <= N / 2) continue;

            puzzle[r][c] = 0;
            var t = Clone(puzzle);
            if (CountSolutions(t, 2) != 1) puzzle[r][c] = bak;
        }
        return (puzzle, solved);
    }

    public bool IsCompleteAndValid(int[][] g)
    {
        for (int r = 0; r < N; r++)
        {
            var row = new HashSet<int>();
            var col = new HashSet<int>();
            for (int c = 0; c < N; c++)
            {
                int vr = g[r][c], vc = g[c][r];
                if (vr < 1 || vr > N || !row.Add(vr)) return false;
                if (vc < 1 || vc > N || !col.Add(vc)) return false;
            }
        }
        for (int br = 0; br < N; br += BoxRows)
            for (int bc = 0; bc < N; bc += BoxCols)
            {
                var box = new HashSet<int>();
                for (int rr = 0; rr < BoxRows; rr++)
                    for (int cc = 0; cc < BoxCols; cc++)
                    {
                        int v = g[br + rr][bc + cc];
                        if (v < 1 || v > N || !box.Add(v)) return false;
                    }
            }
        return true;
    }
}

public enum Difficulty { Facil, Media, Dificil }
public static class DifficultyClues
{
    public static (int min, int max) RangeFor(this Difficulty d, int n)
    {
        int total = n * n;
        return d switch
        {
            Difficulty.Facil => ((int)(total * 0.65), (int)(total * 0.80)),
            Difficulty.Media => ((int)(total * 0.50), (int)(total * 0.65)),
            _ => ((int)(total * 0.35), (int)(total * 0.50)),
        };
    }
}
