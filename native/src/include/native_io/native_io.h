#pragma once
#include <cstdint>
#include <string>
#include <vector>

#if _WIN32
#define CEAD __declspec(dllexport)
#else
#define CEAD
#endif

extern "C" {
CEAD void GetVectorHandle(std::vector<std::uint8_t>* vector, std::uint8_t** dst, std::uint32_t* dst_len);
CEAD bool FreeVectorHandle(std::vector<std::uint8_t>* vector);

CEAD void GetStringHandle(std::string* str, const char** dst, std::uint32_t* dst_len);
CEAD bool FreeStringHandle(std::string* str);
}
