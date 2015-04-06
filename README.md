# dcpu16
A  DCPU-16 assembler &amp; emulator in C#.

DCPU-16 assembler and emulator, that was supposedly featured in n0tch's sandbox science fiction video game called [0x10<sup>c</sup>](http://en.wikipedia.org/wiki/0x10c). 

Based on [spec ver 1.7](https://github.com/abkolan/dcpu16/blob/master/dcpu-1-7.txt)

## USAGE ##
1. Create a binary dump of a DCPU 16 asm program 

	` assembler.exe HelloWorld.asm HelloWorld.bin`

1. Execute the binary dump in the DCPU-16 emulator

    `emulator.exe HelloWorld.bin` (launches in the same console window)
    
    `start emulator.exe HelloWorld.bin` (launches in a new console window)

## VERSION HISTORY ##
**0.01 ALPHA**

Well nothing is really in place, there are a lot of issues (I know right ?), the basic 
framework is up and can be used to print a "hell oh! world"

**KNOWN ISSUES**

- Supports only Hex Address / Values.
- Stack Operations not implemented.
- Overflow / underflow operations are not implemented.
- JSR is not implemented.
- KeyBoard is not Implemented.
- Any operations that look like [A+1] or [0x8000 + 3] is not implemented.
- Comments are not supported.
- MLI, DVI, MDI,SHR, IFA,IFU, ADX, SBX are not implemented.
- And a lot of unknown issues.

##HUGS & BUGS##
[abkolan@gmail.com](mailto:abkolan@gmail.com "email me") | [@abkolan ](http://twitter.com/abkolan "Tweet!")
