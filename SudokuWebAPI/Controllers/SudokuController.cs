using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SudokuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        
        [HttpPost]
        public Sudoku Post([FromBody] Sudoku sudoku)
        {
            sudoku.board = Sudoku.Solve(sudoku.board);
            
            return sudoku;
        }

        
    }
}
