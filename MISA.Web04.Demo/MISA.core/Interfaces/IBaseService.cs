namespace MISA.core.Interfaces
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Base thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertService(MISAEntity entity);

        /// <summary>
        /// Base update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateService(MISAEntity entity);
    }
}