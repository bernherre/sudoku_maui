using Sudoku.Engine;
using Xunit;

namespace Sudoku.Tests;

public class SudokuEngineTests
{
    [Fact]
    public void Genera_Resuelto_4x4_Valido()
    {
        var eng = new SudokuEngine(4);
        var solved = eng.Empty();
        Assert.True(eng.Solve(solved));
        Assert.True(eng.IsCompleteAndValid(solved));
    }

    [Fact]
    public void Puzzle_6x6_Solucion_Valida()
    {
        var eng = new SudokuEngine(6);
        var (puzzle, solved) = eng.GeneratePuzzle(20);
        Assert.True(eng.IsCompleteAndValid(solved));
    }
}
