#!/bin/zsh

echo "in start script"
#zsh ./inner/go.sh
zsh $(dirname ${(%):-%N/})/inner/go.sh
