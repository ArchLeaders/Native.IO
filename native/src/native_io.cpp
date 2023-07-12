#include "native_io/native_io.h"

void GetVectorHandle(std::vector<std::uint8_t>* vector, std::uint8_t** dst, std::size_t* dst_len) {
  *dst = vector->data();
  *dst_len = vector->size();
}

bool FreeVectorHandle(std::vector<std::uint8_t>* vector) {
  delete vector;
  return true;
}

void GetStringHandle(std::string* str, const char** dst, std::size_t* dst_len) {
  *dst = str->c_str();
  *dst_len = str->length();
}

bool FreeStringHandle(std::string* str) {
  delete str;
  return true;
}

void GetExceptionHandle(std::exception* ptr, const char** dst) {
  *dst = ptr->what();
}

void FreeExceptionHandle(std::exception* ptr) {
  delete ptr;
}