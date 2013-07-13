#include <stdlib.h>
#include <stdint.h>
#import <Foundation/Foundation.h>
#import "iPhone_target_Prefix.pch"

struct Result
{
    int x, y;
    const char* msg;
};

extern "C" void CallPlugins(const char* key)
{
    UnitySendMessage("ptest", "PlistCallback", key);
}

extern "C" Result GetResult (const char* key)
{
    Result r;
    r.msg = "hoge";
    r.x = 120;
    r.y = 1024;
    
    return r;
}