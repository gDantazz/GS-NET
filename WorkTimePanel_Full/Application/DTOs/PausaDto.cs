namespace WorkTimePanelFull.Application.DTOs
{
    public class PausaDto
    {
        public int Id { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime? Fim { get; set; }
        public int? Duracao { get; set; }
    }
}
