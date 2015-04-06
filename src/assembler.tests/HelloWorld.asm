SET I, 0x8000
SET X, data
:loop
IFE [X], 0x0000
SET PC, end
SET A, [X]
BOR A, 0x7000
SET [I],A
ADD X, 0x0001
ADD I, 0x0001
SET PC, loop

:end
SET PC, end

:data
DAT "Hello, World!",0