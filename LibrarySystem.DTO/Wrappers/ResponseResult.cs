using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Shared.Wrappers;

public record ResponseResult<T>(bool IsSucceed,T Model,string Message,Exception? Ex = null);
public record ResponseResult(bool IsSucceed,string Message,Exception? Ex=null);

