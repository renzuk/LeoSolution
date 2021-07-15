using System;


namespace LeoSolution.Model
{
    public class UsuarioRetorno
    {
        public Usuario usuario { get; internal set; }
        public bool autenticado { get; internal set; }
        public string token { get; internal set; }
        public DateTime dataExpiracao { get; internal set; }
    }
}
