#pragma once
#include <cstdint>
#include <string>
#include <vector>

#if _WIN32
#define CEAD extern "C" __declspec(dllexport)
#else
#define CEAD extern "C"
#endif

CEAD void GetVectorHandle(std::vector<std::uint8_t>* vector, std::uint8_t** dst, std::size_t* dst_len);
CEAD bool FreeVectorHandle(std::vector<std::uint8_t>* vector);

CEAD void GetStringHandle(std::string* str, const char** dst, std::size_t* dst_len);
CEAD bool FreeStringHandle(std::string* str);
