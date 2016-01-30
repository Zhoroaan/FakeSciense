using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ResourceTypes {
    Tree,
    Coal,
    Iron,
    Beryllium,
    Electricity,
    Silicon,
    Stone,
    _Count
}

public class BuildResource {
    public ResourceTypes Type { get; set; }
    public string Name { get; set; }
    public Int64 Count { get; set; }
    public static BuildResource[] Resources {
        get {
            return instance.resources;
        }
    }

    public static BuildResource GetResource(ResourceTypes type) {
        return instance.resources[(int)type];
    }

    private static BuildResource instance = new BuildResource();
    BuildResource[] resources;

    private BuildResource() {
        resources = new BuildResource[] {
            new BuildResource() { Type = ResourceTypes.Tree, Name = "Trees" },
            new BuildResource() { Type = ResourceTypes.Coal, Name = "Coal" },
            new BuildResource() { Type = ResourceTypes.Iron, Name = "Iron" },
            new BuildResource() { Type = ResourceTypes.Beryllium, Name = "Beryllium" },
            new BuildResource() { Type = ResourceTypes.Electricity, Name = "Electricity" },
            new BuildResource() { Type = ResourceTypes.Silicon, Name = "Silicon" },
            new BuildResource() { Type = ResourceTypes.Stone, Name = "Stone" }
        };
    }
}
