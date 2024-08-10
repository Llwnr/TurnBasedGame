using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class PredefinedAssemblyUtil{
    enum AssemblyType{
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpEditorFirstPass,
        AssemblyCSharpFirstPass
    }

    static AssemblyType? GetAssemblyType(string assemblyName){
        switch(assemblyName){
            case "Assembly-CSharp": return AssemblyType.AssemblyCSharp;
            case "Assembly-CSharp-Editor": return AssemblyType.AssemblyCSharpEditor;
            case "Assembly-CSharp-Editor-firstpass": return AssemblyType.AssemblyCSharpEditorFirstPass;
            case "Assembly-CSharp-firstpass": return AssemblyType.AssemblyCSharpFirstPass;
            default: return null;
        }
    }

    static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType){
        if(assembly==null) return;
        for(int i=0; i<assembly.Length; i++){
            Type type = assembly[i];
            if(type != interfaceType && interfaceType.IsAssignableFrom(type)){
                types.Add(type);
            }
        }

    }

    //Will return a list of all the types/classes that implements the given interface in an assembly/s
    public static List<Type> GetTypes(Type interfaceType){
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType,Type[]>();
        List<Type> types = new List<Type>();
        for(int i=0; i<assemblies.Length; i++){
            AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
            if(assemblyType != null){
                assemblyTypes.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
            }
        }

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
        AddTypesFromAssembly(assemblyCSharpTypes, types, interfaceType);

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
        AddTypesFromAssembly(assemblyCSharpFirstPassTypes, types, interfaceType);

        return types;
    }
}
