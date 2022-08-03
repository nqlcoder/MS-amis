using MISA.core.Exceptions;
using MISA.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        protected IBaseRepository<MISAEntity> Repository;
        protected List<string> ErrorValidateMsgs;
        public BaseService(IBaseRepository<MISAEntity> repository)
        {
            Repository = repository;
            ErrorValidateMsgs = new List<string>();
        }
        /// <summary>
        /// Base thêm mới: kiểm tra đã validate dữ liệu hay chưa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NQLINH (18/6/2022)
        public int InsertService(MISAEntity entity)
        {
            // Thực hiện validate dữ liệu
            var isValid = Validate(entity);
            if (isValid == true)
            {
                // Thực hiện thêm mới vào db
                var res = Repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", ErrorValidateMsgs);
            }
        }

        /// <summary>
        /// Base update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NQLINH (18/6/2022)
        public int UpdateService(MISAEntity entity)
        {
            // Thực hiện validate dữ liệu
            var isValid = ValidateUpdate(entity);
            if (isValid == true)
            {
                // Thực hiện update vào db
                var res = Repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", ErrorValidateMsgs);
            }
        }

        protected virtual bool Validate(MISAEntity entity)
        {
            return true;
        }
        protected virtual bool ValidateUpdate(MISAEntity entity)
        {
            return true;
        }
    }
}
