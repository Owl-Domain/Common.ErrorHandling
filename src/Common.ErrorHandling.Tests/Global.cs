global using System;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using OwlDomain.Testing.Assertions;
global using Assert = OwlDomain.Testing.MSTest.Assert;

using System.Diagnostics.CodeAnalysis;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 1)]
#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#endif

[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Test method names do not need to conform to regular standards.")]