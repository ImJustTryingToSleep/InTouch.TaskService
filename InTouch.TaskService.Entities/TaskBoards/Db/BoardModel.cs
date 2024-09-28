namespace InTouch.TaskService.Common.Entities.TaskBoards
{
    public class BoardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public IAsyncEnumerable<ColumnModel> Columns { get; set; }
    }
}
