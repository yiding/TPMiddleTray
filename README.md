# TPMiddleTray
Utility to allow middle click on Thinkpad Trackpoint to both act as scrolling
and middle click.

Get the binary from the releases page.

Requirements:

- Recent version of Windows 10
- Thinkpad with a Synaptics trackpoint
- Synaptics driver installed

## Instructions

Configure middle button action to scroll:

Mouse Settings > Additional mouse options > Thinkpad > Middle Button Action >
Use for scrolling

Middle click without moving the trackpoint sends a middle click event if the
button is held down for less than 250ms.

## Supported Versions

Tested on:

- Thinkpad X1 Carbon Gen 6

## Developer Details

This tool communicates with the Synaptics COM API that's installed with the
Synaptics driver (SYNCOM). It does using a C# Runtime Callable Wrapper (RCW)
generated from the COM API using `tlbimp` and `ILSpy`, followed by some hand
editing to use more marshalling features. The constants are found from
Synaptics API header files scattered around the web (Synaptics no longer offers
the SDK for download from their website).


Inspired by the tpmiddle utility: https://sdx1.net/tools/tpmiddle/
