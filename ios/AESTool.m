//
//  AESTool.m
//  WhyAes
//
//  Created by why on 6/19/14.
//  Copyright (c) 2014 why. All rights reserved.
//

#import "AesTool.h"
#import "FBEncryptorAES.h"
#import "GTMBase64.h"

@implementation AESTool


+(NSString*)encryptData:(NSString *)data withKey:(NSString *)key
{
    NSData *data_aes = [FBEncryptorAES encryptData:[data dataUsingEncoding:NSUTF8StringEncoding]
                                               key:[key dataUsingEncoding:NSASCIIStringEncoding]];
    
    return [NSString stringWithFormat:@"%@",[GTMBase64 stringByEncodingData:data_aes]];
    
}




+(NSString*)decryptData:(NSString *)data withKey:(NSString *)key
{
    NSData *data_dec = [FBEncryptorAES decryptData:[GTMBase64 decodeString:data]
                                               key:[key dataUsingEncoding:NSASCIIStringEncoding]];
    
    return [[NSString alloc]initWithData:data_dec encoding:NSUTF8StringEncoding];
    
}

@end
