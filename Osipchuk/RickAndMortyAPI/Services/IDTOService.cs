namespace RickAndMortyAPI.IOHandler
{
    public interface IDTOService<T,D>
    {
        public D GetDTO(T obj);
        public IEnumerable<D> GetDTOs(IEnumerable<T> objs);
    }
}
