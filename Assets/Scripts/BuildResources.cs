using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ResourceType {
    Stone,
    Tree,
    Coal,
    Iron,
    Electricity,
    Silicon,
    Beryllium,
    _Count
}

public class BuildResource {
    public ResourceType Type { get; set; }
    public string Name { get; set; }
    public Int64 Count { get; set; }
    public static BuildResource[] Resources {
        get {
            return instance.resources;
        }
    }

    public static Int64 FreeWorkers {
        get {
            return instance.Count;
        }
        set {
            instance.Count = value;
        }
    }

    public static Int64 CountWorkers {
        get {
            var numberOfWorkers = instance.Count;
            foreach(var resource in instance.resources) {
                numberOfWorkers += resource.Count;
            }
            return numberOfWorkers;
        }
    }

    public static BuildResource GetResource(ResourceType type) {
        return instance.resources[(int)type];
    }

    private static BuildResource instance = new BuildResource(true);
    BuildResource[] resources;

    private BuildResource(bool initilizedResource) {
        if(initilizedResource) {
            Count = 100000;
            resources = new BuildResource[] {
                new BuildResource(false) { Type = ResourceType.Tree, Name = "Trees" },
                new BuildResource(false) { Type = ResourceType.Coal, Name = "Coal" },
                new BuildResource(false) { Type = ResourceType.Iron, Name = "Iron" },
                new BuildResource(false) { Type = ResourceType.Beryllium, Name = "Beryllium" },
                new BuildResource(false) { Type = ResourceType.Electricity, Name = "Electricity" },
                new BuildResource(false) { Type = ResourceType.Silicon, Name = "Silicon" },
                new BuildResource(false) { Type = ResourceType.Stone, Name = "Stone" }
            };
        }
    }
}
