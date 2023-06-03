cmake_minimum_required(VERSION 3.10)
project(native_io VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)
set(CMAKE_STATIC_LIBRARY_PREFIX "")
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)
set(CMAKE_SHARED_LIBRARY_PREFIX "")

include(CTest)
enable_testing()

add_library(native_io SHARED
    ./src/native_io.cpp
    ./src/include/native_io/native_io.h
)

target_include_directories(native_io PUBLIC ./src/include)

if(MSVC)
    target_compile_options(native_io PRIVATE /W4 /wd4244 /wd4127 /Zc:__cplusplus)
else()
    target_compile_options(native_io PRIVATE -Wall -Wextra -fno-plt)
endif()

if(WIN32)
    set(CMAKE_CXX_STANDARD_LIBRARIES "-static -static-libgcc -static-libstdc++")
endif()

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
