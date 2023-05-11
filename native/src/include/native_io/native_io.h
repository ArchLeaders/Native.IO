#pragma once
#include <cstdint>
#include <string>
#include <vector>

#if _WIN32
#define EXP extern "C" __declspec(dllexport)
#else
#define EXP extern "C"
#endif

EXP void GetVectorHandle(std::vector<std::uint8_t>* vector, std::uint8_t** dst, std::size_t* dst_len);
EXP bool FreeVectorHandle(std::vector<std::uint8_t>* vector);

EXP void GetStringHandle(std::string* str, const char** dst, std::size_t* dst_len);
EXP bool FreeStringHandle(std::string* str);
