var sessionData = {
    title: "Bait & Switch",
    preparation:[
        "Presentation with Architectural Graphics",
        "Have nuget.exe in System Path",
        "Create local File System NuGet source and configure in Visual Studio"
    ],
    innerTopics: [{
        name: "Cross Platform",
        innerTopics: [{
            name: "Why cross platform?",
            duration: 2
        }, {
            name: "CLR & other runtimes",
            innerTopics: [{
                name: ".Net Framewokr",
                duration: 0
            }, {
                name: "Mono (Xamarin)",
                duration: 2
            }, {
                name: ".Net Core",
                duration: 1
            }]
        }, {
            name: "PCL",
            innerTopics: [{
                name: "What is PCL?",
                duration: 3
            }, {
                name: "Platforms & Profiles",
                duration: 5
            }]
        }, {
            name: ".Net Standard",
            innerTopics: [{
                name: "What is .Net Standard",
                duration: 3
            }, {
                name: "Supported Platforms (runtimes)",
                duration: 3
            }, {
                name: "Version and lifetime",
                duration: 3
            }]
        }, {
            name: "PCL vs .Net Standard",
            duration: 3
        }]
    }, {
        name: "Bait & Switch",
        innerTopics: [{
            name: "Concept",
            duration: 1
        }, {
            name: "How it works",
            innerTopics: [{
                name: "Cross platform library abstraction",
                duration: 2
            }, {
                name: "Platform specific implementation library",
                duration: "2"
            }]
        }, {
            name: ".Net Core Architecture",
            duration: 5
        }, {
            name: "Code Demo",
            innerTopics: [{
                name: "Create Solution & Projects",
                duration: 2
            }, {
                name: "Implement cross and platform library",
                duration: 3
            }, {
                name: "Implement platform specific libraries",
                duration: 5
            }, {
                name: "Reference libraries in platform specific apps",
                duration: 2
            }, {
                name: "Run & Debug",
                duration: 5
            }]
        }]
    }, {
        name: "NuGet",
        innerTopics: [{
            name: "What is NuGet & the Package Manager",
            duration: 1
        }, {
            name: "How libraries are chosen",
            duration: 5
        }, {
            name: "Code Demo",
            innerTopics: [{
                name: "Create .nuspec",
                duration: 5
            }, {
                name: "Create NuGet package",
                duration: 1
            }, {
                name: "Add to NuGet package source",
                duration: 1
            }, {
                name: "Reference package in the projects",
                duration: 3
            }, {
                name: "Run & Debug",
                duration: 5
            }]
        }]
    }, {
        name: ".Net Foundation",
        duration:5
    }, {
        name: "Q&A"
    }]
};

function SumDurations(parent){
    var result = 0;
    parent.innerTopics.forEach(function(topic){
        
    })

    return result;
}

function SetDurations(){
        sessionData.duration = SumDurations(sessionData);
}