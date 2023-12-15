namespace CNPM_BE.Models
{
    public class ApiResponseExpose<Entity> : ApiResp
    {
        /// <summary>
        /// dữ liệu trả về
        /// </summary>
        public Entity? entity { get; set; }
    }
}
