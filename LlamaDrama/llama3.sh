#!/bin/sh

/bin/ollama serve &
sleep 5s
/bin/ollama run llama3
wait
